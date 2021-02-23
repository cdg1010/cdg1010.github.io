using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace Unity.Reflect.Model
{
    public static class Utils
    {

        /// <summary>
        /// Fills the RepeatedField with the provided content.
        /// </summary>
        /// <param name="repeated">The RepeatedField that will be filled.</param>
        /// <param name="values">The content to set into the RepeatedField.</param>
        public static void Set<T>(this RepeatedField<T> repeated, IEnumerable<T> values)
        {
            repeated.Clear();
            if (values != null)
                repeated.AddRange(values);
        }

        /// <summary>
        /// Fills the RepeatedField with the provided content.
        /// </summary>
        /// <param name="repeated">The RepeatedField that will be filled.</param>
        /// <param name="values">The content to set into the RepeatedField.</param>
        public static void Set(this RepeatedField<string> repeated, IEnumerable<string> values)
        {
            repeated.Clear();
            if (values != null)
                repeated.AddRange(values.Select(NotNullString));
        }

        /// <summary>
        /// Fills the MapField with the provided content.
        /// </summary>
        /// <param name="mapField">The MapField that will be filled.</param>
        /// <param name="values">The content to set into the MapField.</param>
        public static void Set<TKey, TValue>(this MapField<TKey, TValue> mapField, IDictionary<TKey, TValue> values)
        {
            mapField.Clear();
            if (values != null)
            {
                foreach (var val in values)
                {
                    mapField.Add(val.Key, val.Value);
                }
            }
        }

        public static string PersistentHash(this ISyncModel syncModel)
        {
            var proto = (IMessage)syncModel;
            var stream = proto.ToByteArray();
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        internal static string NotNullString(string value)
        {
            return value ?? string.Empty;
        }
    }
}
