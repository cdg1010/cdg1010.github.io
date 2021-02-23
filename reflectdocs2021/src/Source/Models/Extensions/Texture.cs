using Unity.Reflect.Services;

namespace Unity.Reflect.Model
{
    public partial class SyncTexture : ISyncModel, ISyncSendable
    {
        public static string Extension = ".SyncTexture";

        void ISyncSendable.Send(SyncAgent.SyncAgentClient client, string sourceId)
        {
            client.SendTexture(new TextureAsset
            {
                SourceId = sourceId,
                Texture = this,
                Hash = this.PersistentHash()
            });
        }
    }
}
