using System;
using UnityEngine;

namespace Moonee.MoonSDK.Internal
{
    public static class MooneeLog
    {
        private static MooneeLogLevel _logLevel;

        private const string TAG = "Moon SDK";
        
        public static void Initialize(MooneeLogLevel level)
        {
            _logLevel = level;
            EnableLogs();
        }

        public static void Log(string tag, string message)
        {
            if (!Application.isEditor || _logLevel >= MooneeLogLevel.DEBUG)
                Debug.Log(Format(tag, message));
        }

        public static void LogE(string tag, string message)
        {
            if (!Application.isEditor || _logLevel >= MooneeLogLevel.ERROR)
                Debug.LogError(Format(tag, message));
        }

        public static void LogW(string tag, string message)
        {
            if (!Application.isEditor || _logLevel >= MooneeLogLevel.WARNING)
                Debug.LogWarning(Format(tag, message));
        }
        
        private static string Format(string tag, string message) => $"{DateTime.Now} - {TAG}/{tag}: {message}";

        // Separate Log enable/disable separate from VoodooLogLevel 
        public static void DisableLogs()
        {
            Debug.unityLogger.logEnabled = false;
        }

        private static void EnableLogs()
        {
            Debug.unityLogger.logEnabled = true;
        }

        public static bool IsLogsEnabled() => Debug.unityLogger.logEnabled;
    }

    public enum MooneeLogLevel
    {
        ERROR=0,
        WARNING=1,
        DEBUG=2
    }
}