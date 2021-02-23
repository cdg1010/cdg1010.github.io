using Unity.Reflect.Model;

namespace Unity.Reflect.Services
{
    partial class PrefabAsset : ISyncModelAsset
    {
        public ISyncModel Model => Prefab;
    }
    
    partial class ObjectAsset : ISyncModelAsset
    {
        public ISyncModel Model => Object;
    }
    
    partial class MeshAsset : ISyncModelAsset
    {
        public ISyncModel Model => Mesh;
    }
    
    partial class MaterialAsset : ISyncModelAsset
    {
        public ISyncModel Model => Material;
    }
    
    partial class TextureAsset : ISyncModelAsset
    {
        public ISyncModel Model => Texture;
    }

    partial class ObjectInstanceAsset
    {
    }
}
