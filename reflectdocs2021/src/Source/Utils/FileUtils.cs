using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unity.Reflect.Utils
{
    /// <summary>
    /// This class provides some utility methods for file handling.
    /// </summary>
    public static class FileUtils
    {
        static readonly string k_InvalidChars = @"[\\\/:\""*?<>|]+";

        /// <summary>
        /// Makes a "dirty name" sanitized for any file system.
        /// </summary>
        /// <param name="dirtyName">The file name that you want to sanitize (without extension)</param>
        /// <param name="maxLength">The maximum length of the sanitized string</param>
        /// <param name="maxLengthTolerance">The sanitization process starts truncating if the string size exceeds maxLength + maxLengthTolerance</param>
        /// <returns>A sanitized name</returns>
        public static string SanitizeName(string dirtyName, int maxLength = 50, int maxLengthTolerance = 6)
        {
            var name = Regex.Replace(dirtyName, k_InvalidChars, "_");

            name = name.Trim();

            var truncate = name.Length > maxLength + maxLengthTolerance;

            if (truncate)
            {
                var result = new StringBuilder(name.Length);
                result.Append(name.Substring(0, maxLength));
                result.Append("...");

                return result.ToString();
            }

            return name;
        }

        internal static string GetFileMD5Hash(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                using (var md5 = MD5.Create())
                {
                    var hash = md5.ComputeHash(stream);
                    // TODO refactor with CloudStorage.Store()
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        // Directory.Move does not work on a file basis and needs the destination to not exists which is not what we need.
        // https://stackoverflow.com/questions/2553008/directory-move-doesnt-work-file-already-exist
        internal static void MoveDirectoryContent(string source, string target)
        {
            var sourcePath = source.Replace('\\', '/').TrimEnd('/', ' ');
            var targetPath = target.Replace('\\', '/').TrimEnd('/', ' ');
            var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                .GroupBy(Path.GetDirectoryName);

            Task.Run(() =>
            {
                Parallel.ForEach(files, folder =>
                {
                    var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                    Directory.CreateDirectory(targetFolder);
                    foreach (var file in folder)
                    {
                        var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                        if (File.Exists(targetFile)) File.Delete(targetFile);
                        File.Move(file, targetFile);
                    }
                });

                Directory.Delete(source, true);
            });
        }

        // https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
        internal static void CopyDirectoryContent(string source, string target)
         {
             foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                 Directory.CreateDirectory(dirPath.Replace(source, target));

             foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                 File.Copy(newPath, newPath.Replace(source, target), true);
         }
    }
}
