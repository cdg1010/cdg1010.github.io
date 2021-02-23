using System;
using System.Reflection;
using Unity.Reflect.Model;

namespace Unity.Reflect.Data
{
    public struct PersistentKey
    {
        public readonly string typeName;  // Protobuf message type e.g. SyncObject
        public readonly string name;      // E.g. for Sketchup, "Geometry [Model]"
        
        public static PersistentKey Invalid = new PersistentKey(string.Empty, null);
        
        PersistentKey(MemberInfo type, string name)
        {
            typeName = GetTypeName(type);
            this.name = name;
        }

        public static string GetTypeName(MemberInfo type)
        {
            return type.Name;
        }
        
        public PersistentKey(string typeName, string name)
        {
            this.typeName = typeName;
            this.name = name;
        }
        
        public static PersistentKey GetKey(ISyncModel model)
        {
            return new PersistentKey(model.GetType(), model.Name);
        }
        
        public static PersistentKey GetKey<T>(string name) where T : ISyncModel
        {
            return new PersistentKey(typeof(T), name);
        }

        public bool Equals(PersistentKey other)
        {
            return string.Equals(typeName, other.typeName) && string.Equals(name, other.name);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PersistentKey other && Equals(other);
        }

        public static bool operator == (PersistentKey k1, PersistentKey k2)
        {
            return k1.Equals(k2);
        }

        public static bool operator != (PersistentKey k1, PersistentKey k2)
        {
            return !(k1 == k2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((typeName != null ? typeName.GetHashCode() : 0) * 397) ^ (name != null ? name.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"<[{typeName}][{name}]>";
        }

        public static string Encode(PersistentKey key)
        {
            return key.typeName + ";" + key.name;
        }

        public static PersistentKey Decode(string str)
        {
            var values = str.Split(';');
            return new PersistentKey(values[0], values[1]);
        }
    }
}