using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Unity.Reflect.Utils;

namespace Unity.Reflect.Services.Client
{
    class WorkerTask
    {
        public Action action = null;
        public bool blocking = false;
        public bool lastTask = false;
        public TaskCompletionSource<bool> promise;
    }

    class WorkerQueue : IDisposable
    {
        ConcurrentQueue<WorkerTask> m_queue = new ConcurrentQueue<WorkerTask>();
        ManualResetEvent m_queueNotEmpty = new ManualResetEvent(false);
        ManualResetEvent m_noBlockingTask = new ManualResetEvent(true);
        Task[] m_workers;
        int m_nbWorkers;
        bool m_ended = false;

        object m_queueLocker = new object();
        object m_blockLocker = new object();

        public WorkerQueue(int nbWorkers)
        {
            m_nbWorkers = nbWorkers;
        }

        public void Start()
        {
            m_workers = new Task[m_nbWorkers];
            for (var i = 0; i < m_nbWorkers; i++)
            {
                m_workers[i] = Task.Run((Action) Work);
            }
        }

        public void Abort()
        {
            lock(m_queueLocker)
            {
                while (!m_queue.IsEmpty)
                {
                    m_queue.TryDequeue(out var result);
                    result.promise.SetCanceled();
                }
                m_queueNotEmpty.Reset();
                m_noBlockingTask.Set();
            }
        }

        public Task Enqueue(Action action, bool blocking = false, bool lastTask = false)
        {
            var promise = new TaskCompletionSource<bool>();
            lock(m_queueLocker)
            {
                m_queue.Enqueue(new WorkerTask {
                    action = action,
                    blocking = blocking,
                    lastTask = lastTask,
                    promise = promise
                });
                m_queueNotEmpty.Set();
            }
            return promise.Task;
        }

        public void Dispose()
        {
            m_queueNotEmpty.Close();
            m_noBlockingTask.Close();
            foreach (var worker in m_workers)
            {
                worker.Dispose();
            }
        }

        void Work()
        {
            while (true)
            {
                m_queueNotEmpty.WaitOne();

                WorkerTask value;
                bool valueFound;
                lock (m_blockLocker)
                {
                    m_noBlockingTask.WaitOne();
                    lock (m_queueLocker)
                    {
                        valueFound = m_queue.TryDequeue(out value);
                        if (!valueFound && !m_ended)
                            m_queueNotEmpty.Reset();
                    }
                    if (valueFound && value.blocking)
                        m_noBlockingTask.Reset();
                }

                if (m_ended)
                {
                    if (valueFound)
                        Logger.Error("A task has been ignored in the WorkerQueue.");
                    return;
                }

                if (valueFound)
                {
                    value.action?.Invoke();
                    value.promise.SetResult(true);
                    if (value.blocking)
                        m_noBlockingTask.Set();
                    if (value.lastTask)
                    {
                        m_ended = true;
                        m_queueNotEmpty.Set();
                    }
                }
            }
        }
    }
}
