using System;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;
using Unity.Reflect.Model;

namespace Unity.Reflect.IO
{
    /// <summary>
    /// This class provides some methods to save or load models from files.
    /// </summary>
    public static class File
    {

        /// <summary>
        /// Saves a model in a file.
        /// </summary>
        /// <param name="model">The model to save</param>
        /// <param name="path">The path of the file to create or overwrite</param>
        public static void Save(ISyncModel model, string path)
        {
            var message = (IMessage)model;
            using (var output = System.IO.File.Create(path))
            {
                message.WriteTo(output);
            }
        }

        /// <summary>
        /// Asynchronously saves a model in a file.
        /// </summary>
        /// <param name="model">The model to save</param>
        /// <param name="path">The path of the file to create or overwrite</param>
        public static async Task SaveAsync(ISyncModel model, string path)
        {
            var message = (IMessage)model;
            var stream = message.ToByteArray();

            using (var sourceStream = new FileStream(path,
                FileMode.OpenOrCreate, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(stream, 0, stream.Length);
            };
        }

        /// <summary>
        /// Loads a model from a file.
        /// </summary>
        /// <typeparam name="T">The type of model that you want to load</typeparam>
        /// <param name="path">The path of the file to read</param>
        /// <returns>A model of the desired type, created by parsing the provided file</returns>
        public static T Load<T>(string path) where T : IMessage, ISyncModel
        {
            var message = Activator.CreateInstance<T>();
            using (var input = System.IO.File.Open(path, FileMode.Open))
            {
                message.MergeFrom(input);
            }

            return message;
        }
    }
}
