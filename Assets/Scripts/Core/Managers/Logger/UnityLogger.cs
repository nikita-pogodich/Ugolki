using UnityEngine;

namespace Core.Managers.Logger
{
    class UnityLogger : ILogger
    {
        public void LogDebug(string message)
        {
            Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        public void LogAssert(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }
    }
}