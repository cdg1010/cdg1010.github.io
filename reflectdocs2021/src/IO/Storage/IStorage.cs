using System;
using System.Collections.Generic;
using Google.Protobuf;
using Unity.Reflect.Data;
using Unity.Reflect.Model;

namespace Unity.Reflect.IO
{
    interface IStorage
    {
        SyncManifest OpenOrCreateManifest();
        void SaveManifest(string projectId, string sourceId, SyncManifest syncManifest);
        SyncManifest LoadManifest(string projectId, string sourceId);
        IEnumerable<SourceProject> LoadProjectManifests(string projectId);
        string Sanitize(string name);
        string Store(ISyncModel syncModel, string projectId, string sourceId, string relativePath);
        T Load<T>(string projectId, string sourceId, string relativePath) where T : class, ISyncModel, IMessage;
    }
}