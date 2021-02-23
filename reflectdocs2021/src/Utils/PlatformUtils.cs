using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Unity.Reflect.Utils
{
    static class PlatformUtils
    {
        public static readonly bool k_IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static readonly bool k_IsOSX     = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        public static readonly bool k_IsLinux   = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static readonly string k_CloudProjectsFolderName = "cloudProjects";

        public static string InitWorkingSubDirectory(string folderName)
        {
            var serviceWorkPath = InitWorkingDirectory();
            var serviceSubPath = $"{serviceWorkPath}/{folderName}";
            if (!Directory.Exists(serviceSubPath))
            {
                Directory.CreateDirectory(serviceSubPath);
            }
            return serviceSubPath;
        }

        public static string InitWorkingDirectory()
        {
            string serviceWorkPath = "";
            if (k_IsWindows)
            {
                serviceWorkPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}/Unity/Reflect";
            }
            else if (k_IsOSX)
            {
                serviceWorkPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Library/Application Support/Unity/Reflect";
            }
            else if (k_IsLinux) // Cloud server
            {
                serviceWorkPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Unity/Reflect";
            }
            else
            {
                Logger.Fatal("Unrecognised platform");
                Environment.Exit(1);
            }
            Logger.Info($"serviceWorkPath: {serviceWorkPath}");
            
            if (!Directory.Exists(serviceWorkPath))
            {
                Directory.CreateDirectory(serviceWorkPath);
            }
            return serviceWorkPath;
        }

        public static string GetLocalSyncServiceConfigPath()
        {
            var workPath = InitWorkingDirectory();
            return $"{workPath}/syncServiceConfig.xml";
        }
    }
}
