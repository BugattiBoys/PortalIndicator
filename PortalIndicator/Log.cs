﻿using System.Diagnostics;

namespace PortalIndicator
{
    internal static class Log
    {

        public static bool IsDebug { get; set; } = false;

        public static void Debug(object message)
        {
            var stackFrameMethod = new StackTrace().GetFrame(1).GetMethod();
            var callingClass = stackFrameMethod.DeclaringType.Name;
            var callingMethod = stackFrameMethod.Name;
            Jotunn.Logger.LogDebug($"[{callingClass}.{callingMethod}]  {message}");
        }

        public static void Info(object message)
        {
            //if (IsDebug)
            //{
            //   Debug(message);
            //;} else
            //{
                Jotunn.Logger.LogInfo(message);
            //}
        }
        public static void Warning(object message) => Jotunn.Logger.LogWarning(message);
        public static void Error(object message) => Jotunn.Logger.LogError(message);
        public static void Fatal(object message) => Jotunn.Logger.LogFatal(message);
    }
}