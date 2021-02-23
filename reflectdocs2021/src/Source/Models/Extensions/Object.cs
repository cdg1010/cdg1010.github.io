using System.Linq;
using Unity.Reflect.Services;

namespace Unity.Reflect.Model
{
    public partial class SyncObject : ISyncModel, ISyncSendable
    {      
        public static string Extension = ".SyncObject";
        
        public SyncObject(string name)
        {
            Name = name;
            Transform = SyncTransform.Identity();
        }

        public bool IsEmpty()
        {   
            if (!string.IsNullOrEmpty(Mesh))
                return false;
            
            if (Lights != null && Lights.Any())
                return false;
            
            if (Rpcs != null && Rpcs.Any())
                return false;
            
            if (Cameras != null && Cameras.Any())
                return false;

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    if (!child.IsEmpty())
                        return false;
                }
            }
            
            return true;
        }

        void ISyncSendable.Send(SyncAgent.SyncAgentClient client, string sourceId)
        {
            client.SendObject(new ObjectAsset
            {
                SourceId = sourceId,
                Object = this,
                Hash = this.PersistentHash()
            });
        }
    }

    public partial class SyncObjectInstance : ISyncModel, ISyncSendable
    {
        void ISyncSendable.Send(SyncAgent.SyncAgentClient client, string sourceId)
        {
            client.SendObjectInstance(new ObjectInstanceAsset
            {
                SourceId = sourceId,
                Instance = this,
            });
        }
    }
    
    public partial class SyncPrefab : ISyncModel
    {
        public static string Extension = ".SyncPrefab";
    }
}
