using Grpc.Core;
using System;
using System.Threading.Tasks;
using Unity.Reflect.Model;

namespace Unity.Reflect.Services.Client
{
    abstract class PublisherClient : ClientBase, IPublisherClient
    {
        protected string m_SourceId;

        bool m_TransactionPending = false;
        bool m_Closed = false;

        public PublisherClient(Channel channel) : base(channel)
        { }

        public void CreateSession(string name, string sourceId, string userId, string projectId, PublisherSettings settings)
        {
            var result = m_Client.Client.BeginSession(new Session()
            {
                Name = name,
                SourceId = sourceId,
                UserId = userId,
                ProjectId = projectId,
                UnitConversion = settings.LengthUnit.ToGrpc(),
                AxisConversion = settings.AxisInversion.ToGrpc(),
                Rules = settings.Rules
            });
            m_SourceId = result.SourceId;
        }

        public void StartTransaction()
        {
            CheckNotClosed();
            if (m_TransactionPending)
                throw new TransactionAlreadyPendingException();

            m_TransactionPending = true;
        }

        public void CommitTransaction()
        {
            CheckNotClosed();
            CheckTransactionPending();

            WaitPush();
            m_TransactionPending = false;
        }

        public void RollbackTransaction()
        {
            // TODO
            // will empty the "send queue" and then block until the GRPC call to Rollback completes
        }

        public void CloseAndWait(bool autoDisconnectClient)
        {
            CheckNotClosed();

            m_Closed = true;
            WaitClose();
            Dispose(autoDisconnectClient);
        }

        public void Abort(bool autoDisconnectClient)
        {
            CheckNotClosed();

            m_Closed = true;
            m_Client.Client.EndSessionAsync(new SessionAsset
            {
                SourceId = m_SourceId
            });
            Dispose(autoDisconnectClient);
        }

        public Task Send(ISyncModel model)
        {
            CheckNotClosed();
            CheckTransactionPending();

            var sendable = model as ISyncSendable;
            if (sendable == null)
                throw new ArgumentException($"The model type {model.Name} is not sendable.");

            return LaunchSend(sendable);
        }

        public Task RemoveObjectInstance(string objectInstanceName)
        {
            CheckNotClosed();
            CheckTransactionPending();

            return LaunchRemoveObjectInstance(objectInstanceName);
        }

        void CheckTransactionPending()
        {
            if (!m_TransactionPending)
                throw new NoTransactionPendingException();
        }

        void CheckNotClosed()
        {
            if (m_Closed)
                throw new SessionClosedException();
        }

        protected abstract void Dispose(bool autoDisconnectClient);

        protected abstract void WaitPush();

        protected abstract void WaitClose();

        protected abstract Task LaunchSend(ISyncSendable sendable);

        protected abstract Task LaunchRemoveObjectInstance(string objectInstanceName);
    }
}
