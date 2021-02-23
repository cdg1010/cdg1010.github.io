using Grpc.Core;
using System;
using System.Threading.Tasks;
using Unity.Reflect.Model;

namespace Unity.Reflect.Services.Client
{
    class PublisherClientAsync : PublisherClient
    {
        WorkerQueue m_WorkerQueue;

        public PublisherClientAsync(Channel channel, int nbWorkers = 1) : base(channel)
        {
            if (nbWorkers <= 0)
                throw new ArgumentException("You can't use less than 1 worker in the async publisher session.");

            m_WorkerQueue = new WorkerQueue(nbWorkers);
            m_WorkerQueue.Start();
        }

        protected override void WaitPush()
        {
            m_WorkerQueue.Enqueue(() => {
                m_Client.Client.Push(new SessionAsset
                {
                    SourceId = m_SourceId,
                });
            }, true).Wait();
        }

        protected override void WaitClose()
        {
            m_WorkerQueue.Enqueue(() => {
                m_Client.Client.EndSession(new SessionAsset
                {
                    SourceId = m_SourceId,
                });
            }, true, true).Wait();
        }

        protected override Task LaunchSend(ISyncSendable sendable)
        {
            return m_WorkerQueue.Enqueue(() => {
                sendable.Send(m_Client.Client, m_SourceId);
            });
        }

        protected override Task LaunchRemoveObjectInstance(string objectInstanceName)
        {
            return m_WorkerQueue.Enqueue(() => {
                m_Client.Client.RemoveObjectInstance(new ObjectInstanceIdAsset
                {
                    SourceId = m_SourceId,
                    Instance = objectInstanceName
                });
            });
        }

        protected override void Dispose(bool autoDisconnectClient)
        {
            Disconnect();
            m_WorkerQueue.Dispose();
        }
    }
}
