using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grpc.Core;
using Unity.Reflect.Data;
using Unity.Reflect.Model;
using Unity.Reflect.Utils;

namespace Unity.Reflect.Services.Client
{
    class PlayerClient : ClientBase, IPlayerClient
    {
        public PlayerClient(Channel channel) : base(channel)
        { }

        public IEnumerable<Reflect.ManifestAsset> GetManifests(string projectId)
        {
            var request = new ManifestsRequest { ProjectId = projectId };
            return m_Client.Client.GetManifests(request)
                .Manifests
                .Select(x => x.ToApi());
        }

        public SyncManifest GetManifest(string projectId, string sourceId)
        {
            return m_Client.Client.GetManifest(new ManifestRequest { ProjectId = projectId, SourceId = sourceId }).Manifest;
        }

        public ISyncModel GetSyncModel(string projectId, string sourceId, string relativePath)
        {
            var request = new SyncModelRequest { ProjectId = projectId, SourceId = sourceId, DstPath = relativePath };
            ISyncModel syncModel = null;

            try
            {
                var extension = Path.GetExtension(relativePath).ToLower();

                if (extension == SyncTexture.Extension.ToLower())
                {
                    syncModel = m_Client.Client.GetTexture(request).Model;
                }
                else if (extension == SyncMaterial.Extension.ToLower())
                {
                    syncModel = m_Client.Client.GetMaterial(request).Model;
                }
                else if (extension == SyncObject.Extension.ToLower())
                {
                    syncModel = m_Client.Client.GetObject(request).Model;
                }
                else if (extension == SyncMesh.Extension.ToLower())
                {
                    syncModel = m_Client.Client.GetMesh(request).Model;
                }
                else if (extension == SyncPrefab.Extension.ToLower())
                {
                    syncModel = m_Client.Client.GetPrefab(request).Model;
                }
                else
                {
                    throw new Exception("Unsupported ISyncModel Extension: '" + extension + "'");
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Error when getting SyncModel '{relativePath}' : {e}");
            }

            return syncModel;
        }
    }
}
