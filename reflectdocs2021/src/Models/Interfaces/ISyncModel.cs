
using Unity.Reflect.Services;

namespace Unity.Reflect.Model
{
    public interface ISyncModel
    {
        string Name { get; set; }
    }

    interface ISyncSendable
    {
        void Send(SyncAgent.SyncAgentClient client, string sourceId);
    }
}