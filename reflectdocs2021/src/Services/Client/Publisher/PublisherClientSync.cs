using Grpc.Core;
using System.Threading.Tasks;
using Unity.Reflect.Model;

namespace Unity.Reflect.Services.Client
{
    class PublisherClientSync : PublisherClient
    {
        public PublisherClientSync(Channel channel) : base(channel)
        { }

        protected override void Dispose(bool autoDisconnectClient)
        {
            Disconnect();
        }

        protected override void WaitPush()
        {
            m_Client.Client.Push(new SessionAsset
            {
                SourceId = m_SourceId,
            });
        }

        protected override void WaitClose()
        {
            m_Client.Client.EndSession(new SessionAsset
            {
                SourceId = m_SourceId,
            });
        }

        protected override Task LaunchSend(ISyncSendable sendable)
        {
            sendable.Send(m_Client.Client, m_SourceId);
            return Task.CompletedTask;
        }

        protected override Task LaunchRemoveObjectInstance(string objectInstanceName)
        {
            m_Client.Client.RemoveObjectInstance(new ObjectInstanceIdAsset
            {
                SourceId = m_SourceId,
                Instance = objectInstanceName
            });
            return Task.CompletedTask;
        }
    }
}
