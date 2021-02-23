using System;
using Unity.Reflect.Utils;

namespace Unity.Reflect.Constants
{
    // Wraps environment variables for cloud server
    class SyncConstants
    {
        // Returns environment variable cast to bool (value must be "True" or "False)
        private static bool getBool(string var)
        {
            string tmp = Environment.GetEnvironmentVariable(var);
            return tmp != null ? bool.Parse(tmp) : false;
        }

        // Returns environment variable cast to int
        private static int getInt(string var)
        {
            string tmp = Environment.GetEnvironmentVariable(var);
            return tmp != null ? int.Parse(tmp) : 0;
        }

        // Returns environment variable as string (no type conversion needed)
        private static string getString(string var)
        {
            return Environment.GetEnvironmentVariable(var);
        }

        // Server
        public static readonly bool   k_IsCloudServer        = getBool("SYNC_CLOUD");
        public static readonly int    k_ServicePort          = getInt("SYNC_SERVICE_PORT");
        public static readonly string k_Bucket               = getString("SYNC_BUCKET");

        // Other .env vars not used in code:
        // - SYNC_GOOGLE_APPLICATION_CREDENTIALS

        // Player (not env vars)
        public static readonly bool   k_IsCloudClient        = false;
        public static readonly string k_PlayerServiceAddress = "127.0.0.1";
        public static readonly string k_PlayerServiceName    = "reflect-cloud";

        static SyncConstants()
        {
            Logger.Debug($"k_IsCloudServer: {k_IsCloudServer}");
            Logger.Debug($"k_ServicePort:   {k_ServicePort}");
            Logger.Debug($"k_Bucket:        {k_Bucket}");
            Logger.Debug($"k_IsCloudClient: {k_IsCloudClient}");
        }
    }
}
