using System.Collections.Generic;

namespace Core.Managers.Logger
{
    public class LogManager
    {
        private static List<ILogger> _registeredLoggers = new List<ILogger>();

        public static void RegisterLogger(ILogger logger)
        {
            if (_registeredLoggers.Contains(logger) == true)
            {
                return;
            }

            _registeredLoggers.Add(logger);
        }

        public static void LogDebug(string message)
        {
            for (int i = 0; i < _registeredLoggers.Count; i++)
            {
                _registeredLoggers[i].LogDebug(message);
            }
        }

        public static void LogWarning(string message)
        {
            for (int i = 0; i < _registeredLoggers.Count; i++)
            {
                _registeredLoggers[i].LogWarning(message);
            }
        }

        public static void LogError(string message)
        {
            for (int i = 0; i < _registeredLoggers.Count; i++)
            {
                _registeredLoggers[i].LogError(message);
            }
        }

        public static void LogAssert(bool condition, string message)
        {
            for (int i = 0; i < _registeredLoggers.Count; i++)
            {
                _registeredLoggers[i].LogAssert(condition, message);
            }
        }
    }
}