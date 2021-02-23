using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Protobuf;
using Unity.Reflect.Data;
using Unity.Reflect.Model;
using Unity.Reflect.Utils;

namespace Unity.Reflect.IO
{
    public class SourceProject
    {
        public string sourceId;
        public SyncManifest manifest;
    }

    /// <summary>
    /// This class helps manage Reflect projects in local storage.
    /// </summary>
    public class LocalStorage : IStorage
    {
        readonly string m_Root;

        /// <summary>
        /// Creates the local storage
        /// </summary>
        /// <param name="root">The path of the root folder (which will hold all the Reflect projects)</param>
        public LocalStorage(string root)
        {
            m_Root = root;
        }

        /// <summary>
        /// Opens or creates a manifest for the root folder.
        /// </summary>
        /// <returns>A manifest that manages the root folder</returns>
        public SyncManifest OpenOrCreateManifest()
        {
            SyncManifest syncManifest = null;
            var manifestPath = SyncManifest.ExpectedManifestPath(m_Root);

            try
            {
                if (System.IO.File.Exists(manifestPath))
                {
                    // Player only
                    Logger.Info($"OpenOrCreateManifest(): Using existing manifest '{manifestPath}'");
                    syncManifest = SyncManifest.LoadFromDisk(manifestPath);
                }
                else
                {
                    // SyncServer only
                    Logger.Info($"OpenOrCreateManifest(): Starting new manifest at '{manifestPath}'");
                    syncManifest = new SyncManifest();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return syncManifest;
        }

        /// <summary>
        /// Save a manifest in a specific source project.
        /// </summary>
        /// <param name="projectId">The Unity Project ID</param>
        /// <param name="sourceProjectId">The source project ID</param>
        /// <param name="syncManifest">The manifest to save</param>
        public void SaveManifest(string projectId, string sourceId, SyncManifest syncManifest)
        {
            string path = GetSourceProjectFolder(projectId, sourceId);
            Logger.Info($"SaveManifest(projectId={projectId}, sourceId={sourceId}): saving manifest to {path}");
            syncManifest.Save(path);
        }

        /// <summary>
        /// Loads all the local source projects in a specific Unity Project.
        /// </summary>
        /// <param name="projectId">The Unity Project ID</param>
        /// <returns>All the source projects that are locally stored in the provided Unity Project.</returns>
        public IEnumerable<SourceProject> LoadProjectManifests(string projectId)
        {
            var sourceIds = GetLocalSourceProjectIds(projectId);

            foreach (var sourceId in sourceIds)
            {
                var sessionFullPath = GetSourceProjectFolder(projectId, sourceId);
                var manifestPath = SyncManifest.ExpectedManifestPath(sessionFullPath);

                if (!System.IO.File.Exists(manifestPath))
                    continue;

                yield return new SourceProject { sourceId = sourceId, manifest = SyncManifest.LoadFromDisk(manifestPath) };
            }
        }

        /// <summary>
        /// Gets all the IDs of all the source projects that are locally stored in an Unity Project.
        /// </summary>
        /// <param name="projectId">The Unity project ID</param>
        /// <returns>An array of source project IDs</returns>
        public string[] GetLocalSourceProjectIds(string projectId)
        {
            var projectFolder = GetProjectFolder(projectId);

            return Directory.Exists(projectFolder) ? GetSubDirectoriesNames(projectFolder) : new string[] { };
        }

        /// <summary>
        /// Loads the manifest for a locally stored source project in an Unity Project.
        /// </summary>
        /// <param name="projectId">The Unity Project ID</param>
        /// <param name="sourceProjectId">The source project ID</param>
        /// <returns>The source project's manifest</returns>
        public SyncManifest LoadManifest(string projectId, string sourceId)
        {
            var manifestPath = SyncManifest.ExpectedManifestPath(GetSourceProjectFolder(projectId, sourceId));
            return SyncManifest.LoadFromDisk(manifestPath);
        }

        /// <summary>
        /// Sanitizes the file name.
        /// </summary>
        /// <param name="name">A file name to sanitize (without extension)</param>
        /// <returns>A sanitized file name</returns>
        public string Sanitize(string name)
        {
            // TODO refactor with CloudStorage.Sanitize
            return FileUtils.SanitizeName(name);
        }

        /// <summary>
        /// Stores a model in a locally stored Source project.
        /// </summary>
        /// <param name="syncModel">The model to store</param>
        /// <param name="projectId">The Unity project ID</param>
        /// <param name="sourceProjectId">The Source project ID</param>
        /// <param name="relativePath">The relative path of the model</param>
        /// <returns>A hash of the created (or overwritten) file's absolute path</returns>
        public string Store(ISyncModel syncModel, string projectId, string sourceId, string relativePath)
        {
            var fullPath = Path.Combine(GetSourceProjectFolder(projectId, sourceId), relativePath);

            var folder = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            File.Save(syncModel, fullPath);

            var hash = FileUtils.GetFileMD5Hash(fullPath);
            return hash;
        }

        /// <summary>
        /// Loads a model from a locally stored source project.
        /// </summary>
        /// <typeparam name="T">The type of the model you want to load</typeparam>
        /// <param name="projectId">The Unity Project ID</param>
        /// <param name="sourceProjectId">The Source project ID</param>
        /// <param name="relativePath">The relative path of the model</param>
        /// <returns>A model of the desired type, created by parsing the locally stored model</returns>
        public T Load<T>(string projectId, string sourceId, string relativePath) where T : class, ISyncModel, IMessage
        {
            var path = Path.Combine(GetSourceProjectFolder(projectId, sourceId), relativePath);

            return !System.IO.File.Exists(path) ? null : File.Load<T>(path);
        }

        /// <summary>
        /// Gets the folder path of a specific Unity Project.
        /// </summary>
        /// <param name="projectId">The ID of the Unity Project</param>
        /// <returns>The folder path of the provided project</returns>
        public string GetProjectFolder(string projectId)
        {
            return $"{m_Root}/{projectId}";
        }

        /// <summary>
        /// Gets the folder path of a specific source project in an Unity Project.
        /// </summary>
        /// <param name="projectId">The ID of the Unity Project</param>
        /// <param name="sourceProjectdId">The ID of the source project</param>
        /// <returns>The folder path of the provided project</returns>
        public string GetSourceProjectFolder(string projectId, string sourcedId)
        {
            return $"{GetProjectFolder(projectId)}/{sourcedId}";
        }

        /// <summary>
        /// Indicates whether there is local data for a specific Unity Project.
        /// </summary>
        /// <param name="projectId">The ID of the Unity Project</param>
        public bool HasLocalData(string projectId)
        {
            // Returns true if at least one manifest is present in the project folder.
            var projectFolder = GetProjectFolder(projectId);
            if (!Directory.Exists(projectFolder))
                return false;

            var manifestPath = Directory.EnumerateFiles(projectFolder, SyncManifest.FileName, SearchOption.AllDirectories).FirstOrDefault();
            return !string.IsNullOrEmpty(manifestPath);
        }

        static string[] GetSubDirectoriesNames(string directory)
        {
            var directories = Directory.GetDirectories(directory);

            for (var i = 0; i < directories.Length; ++i)
            {
                directories[i] = directories[i].Replace(directory, string.Empty).Trim('\\').Trim('/');
            }

            return directories;
        }
    }
}
