using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Protobuf;
using Unity.Reflect.Model;

namespace Unity.Reflect.Data
{
    public partial class SyncManifest
    {
        public static readonly string FileName = "sync.manifest";
        static readonly string k_LocalPath = ".manifest/" + FileName;

        Dictionary<PersistentKey, ManifestEntry> m_Content;

        public IReadOnlyDictionary<PersistentKey, ManifestEntry> Content
        {
            get
            {
                if (m_Content == null)
                {
                    RebuildCache();
                }

                return m_Content;
            }   
        }

        void RebuildCache()
        {
            m_Content = new Dictionary<PersistentKey, ManifestEntry>(Entries.Count);

            foreach (var entry in Entries)
            {
                var persistentKey = PersistentKey.Decode(entry.Key);
                m_Content.Add(persistentKey, entry.Value);
            }
        }

        public Dictionary<PersistentKey, string> Remaps
        {
            get { return Content.ToDictionary(entry => entry.Key, entry => entry.Value.DstPath); }
        }

        public static string ExpectedManifestPath(string destinationFolder)
        {
            return Path.Combine(destinationFolder, k_LocalPath);
        }
        
        public void Append(PersistentKey key, string srcHash, string dstPath, string dstHash, SyncBoundingBox bbox)
        {
            if (m_Content == null)
            {
                RebuildCache();
            }
            
            if (!m_Content.TryGetValue(key, out var data))
            {
                var encodedKey = PersistentKey.Encode(key);
                 Entries[encodedKey] = m_Content[key] = data = new ManifestEntry();
            }
            
            data.SrcHash = srcHash;
            data.DstPath = dstPath;
            data.DstHash = dstHash;
            data.BoundingBox = bbox;
        }

        public void Save(string destinationFolder)
        {
            var manifestPath = ExpectedManifestPath(destinationFolder);
            var folder = Path.GetDirectoryName(manifestPath);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            SaveInternal(manifestPath);
        }
        
        // Save to disk
        void SaveInternal(string path)
        {            
            var message = (IMessage)this;
            using (var output = File.Create(path))
            {
                message.WriteTo(output);
            }
        }

        // Load from disk
        internal static SyncManifest LoadFromDisk(string path)
        {
            var message = Activator.CreateInstance<SyncManifest>();
            using (var input = File.Open(path, FileMode.Open))
            {
                message.MergeFrom(input);
            }

            return message;
        }

        // For cloud - load from memory
        public static SyncManifest LoadFromBytes(byte[] data)
        {
            var message = Activator.CreateInstance<SyncManifest>();
            message.MergeFrom(data);
            return message;
        }
                
        public void ComputeDiff(SyncManifest other, out IList<ManifestEntry> changed, out IList<ManifestEntry> removed)
        {
            changed = new List<ManifestEntry>();
            removed = new List<ManifestEntry>();

            var oldContent = Content;
            var newContent = other.Content;

            ComputeDiff(oldContent, newContent, out changed, out removed);
        }
        
        static void ComputeDiff(IReadOnlyDictionary<PersistentKey, ManifestEntry> oldContent, IReadOnlyDictionary<PersistentKey, ManifestEntry> newContent,
            out IList<ManifestEntry> changed, out IList<ManifestEntry> removed)
        {
            changed = new List<ManifestEntry>();
            removed = new List<ManifestEntry>();

            foreach (var couple in oldContent)
            {
                var persistentKey = couple.Key;

                if (newContent.TryGetValue(persistentKey, out var newData))
                {
                    if (!Compare(couple.Value, newData))
                    {
                        changed.Add(newData);
                    }
                }
                else
                {
                    removed.Add(couple.Value);
                }
            }

            foreach (var couple in newContent)
            {
                var persistentKey = couple.Key;

                if (!oldContent.ContainsKey(persistentKey))
                {
                    changed.Add(couple.Value);
                }
            }
        }

        static bool Compare(ManifestEntry first, ManifestEntry second)
        {
            return first.SrcHash == second.SrcHash
                && first.DstHash == second.DstHash
                && string.Equals(first.DstPath, second.DstPath, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
