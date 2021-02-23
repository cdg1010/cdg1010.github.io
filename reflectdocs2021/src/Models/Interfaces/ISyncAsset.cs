using Unity.Reflect.Model;

namespace Unity.Reflect.Services
{
    interface ISyncModelAsset
    {
        string Hash { get; }
        ISyncModel Model { get; }
    }
}