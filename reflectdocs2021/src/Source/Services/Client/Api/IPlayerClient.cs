using System.Collections.Generic;
using Unity.Reflect.Data;
using Unity.Reflect.Model;

namespace Unity.Reflect
{
    public interface IPlayerClient : IClient
    {
        IEnumerable<ManifestAsset> GetManifests(string projectId);
        SyncManifest GetManifest(string projectId, string sourceId);
        ISyncModel GetSyncModel(string projectId, string sourceId, string relativePath);
    }

    public struct ManifestAsset
    {
        public readonly string ProjectId;
        public readonly string SourceId;
        public readonly SyncManifest Manifest;

        public ManifestAsset(string projectId, string sourceId, SyncManifest manifest)
        {
            ProjectId = projectId;
            SourceId = sourceId;
            Manifest = manifest;
        }
    }
}
