using System.Diagnostics;

namespace BrotatoM
{
    public class Log
    {
        [Conditional("Debug")]
        public static void Debug(string message, int size = -1)
        {
            if (size == -1)
                UnityEngine.Debug.Log("<color=cyan>" + message + "</color>");
            else
                UnityEngine.Debug.Log($"<size={size}><color=cyan>" + message + "</color></size>");
        }

        [Conditional("Debug"), Conditional("Info")]
        public static void Info(string message, int size = -1)
        {
            if (size == -1)
                UnityEngine.Debug.Log("<color=green>" + message + "</color>");
            else
                UnityEngine.Debug.Log($"<size={size}><color=green>" + message + "</color></size>");
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning")]
        public static void Warning(string message, int size = -1)
        {
            if (size == -1)
                UnityEngine.Debug.Log("<color=yellow>" + message + "</color>");
            else
                UnityEngine.Debug.Log($"<size={size}><color=yellow>" + message + "</color></size>");
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning"), Conditional("Error")]
        public static void Error(string message, int size = -1)
        {
            if (size == -1)
                UnityEngine.Debug.Log("<color=red>" + message + "</color>");
            else
                UnityEngine.Debug.Log($"<size={size}><color=red>" + message + "</color></size>");
        }
    }
}
