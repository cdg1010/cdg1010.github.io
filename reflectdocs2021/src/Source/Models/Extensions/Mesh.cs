using System.Linq;
using Unity.Reflect.Services;

namespace Unity.Reflect.Model
{
    public partial class SyncMesh : ISyncModel, ISyncSendable
    {
        public static string Extension = ".SyncMesh";
        
        public SyncMesh(string name, SyncFloat3[] vertices, SyncFloat3[] normals, SyncFloat2[] uvs, SyncSubMesh[] subMeshes)
        {
            Name = name;
            Vertices.Set(vertices.Select(x => x));
            Normals.Set(normals.Select(x => x));
            Uvs.Set(uvs.Select(x => x));
            Submeshes.Set(subMeshes);
        }

        void ISyncSendable.Send(SyncAgent.SyncAgentClient client, string sourceId)
        {
            client.SendMesh(new MeshAsset
            {
                SourceId = sourceId,
                Mesh = this,
                Hash = this.PersistentHash()
            });
        }
    }
    
    public partial class SyncSubMesh
    {
        public SyncSubMesh(int[] triangles)
        {
            Triangles.Set(triangles);
        }
    }
}