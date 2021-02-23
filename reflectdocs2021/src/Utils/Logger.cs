using System;

namespace Unity.Reflect.Utils
{
    /// <summary>
    /// Use this class to write and read logs.
    /// </summary>
    public static class Logger
    {
        public enum Level
        {
            Debug = 0,
            Info = 1,
            Warn = 2,
            Error = 3,
            Fatal = 4
        }

        public delegate void LogReceiver(Level level, string msg);

        /// <summary>
        /// This event is triggered whenever a log request is made. Use this event to display the log at your convenience.
        /// </summary>
        public static event LogReceiver OnLog;

        /// <summary>
        /// This value indicates which minimum level of log triggers the OnLog event.
        /// </summary>
        public static Level minLevel = Level.Debug;
        
        /// <summary>
        /// Logs a message at Info level.
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            Log(Level.Info, msg);
        }

        /// <summary>
        /// Logs a message at Error level.
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Log(Level.Error, msg);
        }

        /// <summary>
        /// Logs a message at Warn level.
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            Log(Level.Warn, msg);
        }

        /// <summary>
        /// Logs a message at Fatal level.
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            Log(Level.Fatal, msg);
        }

        /// <summary>
        /// Logs a message at Debug level.
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            Log(Level.Debug, msg);
        }
        
        static void Log(Level level, string msg)
        {
            if (level >= minLevel)
            {
                OnLog?.Invoke(level, msg);
            }
        }
    }
}
