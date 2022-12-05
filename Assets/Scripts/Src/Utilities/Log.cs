using System.Diagnostics;

namespace BrotatoM
{
    public class Log
    {
        [Conditional("Debug")]
        public static void Debug(string message)
        {
            UnityEngine.Debug.Log("<color=cyan>" + message + "</color>");
        }

        [Conditional("Debug"), Conditional("Info")]
        public static void Info(string message)
        {
            UnityEngine.Debug.Log("<color=green>" + message + "</color>");
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning")]
        public static void Warning(string message)
        {
            UnityEngine.Debug.LogWarning("<color=yellow>" + message + "</color>");
        }

        [Conditional("Debug"), Conditional("Info"), Conditional("Warning"), Conditional("Error")]
        public static void Error(string message)
        {
            UnityEngine.Debug.LogError("<color=red>" + message + "</color>");
        }
    }
}
