using System;
using System.Diagnostics;

namespace BrotatoM
{
    public class Log
    {
        private static string ConstructArrayMessage(object[] arr)
        {
            string msg = "[ ";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                    msg += arr[i].ToString() + " ]";
                else
                    msg += arr[i].ToString() + ", ";
            }
            return msg;
        }

        private static void LogWithColorAndSize(string message, string color, int size)
        {
            if (size == -1)
                UnityEngine.Debug.Log($"<color={color}>" + message + "</color>");
            else
                UnityEngine.Debug.Log($"<size={size}><color={color}>" + message + "</color></size>");
        }

        [Conditional("Debug")]
        public static void Debug(object message, int size = -1)
        {
            LogWithColorAndSize(message.ToString(), "cyan", size);
        }

        [Conditional("Debug")]
        public static void Debug(object[] message, int size = -1)
        {
            LogWithColorAndSize(ConstructArrayMessage(message), "cyan", size);
        }

        [Conditional("Debug"), Conditional("Info")]
        public static void Info(object message, int size = -1)
        {
            LogWithColorAndSize(message.ToString(), "green", size);
        }

        [Conditional("Debug"), Conditional("Info")]
        public static void Info(object[] message, int size = -1)
        {
            LogWithColorAndSize(ConstructArrayMessage(message), "green", size);
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning")]
        public static void Warning(object message, int size = -1)
        {
            LogWithColorAndSize(message.ToString(), "yellow", size);
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning")]
        public static void Warning(object[] message, int size = -1)
        {
            LogWithColorAndSize(ConstructArrayMessage(message), "yellow", size);
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning"), Conditional("Error")]
        public static void Error(object message, int size = -1)
        {
            LogWithColorAndSize(message.ToString(), "red", size);
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning"), Conditional("Error")]
        public static void Error(object[] message, int size = -1)
        {
            LogWithColorAndSize(ConstructArrayMessage(message), "red", size);
        }
    }
}
