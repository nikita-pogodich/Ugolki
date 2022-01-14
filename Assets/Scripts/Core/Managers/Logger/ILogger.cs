namespace Core.Managers.Logger
{
    public interface ILogger
    {
        void LogDebug(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogAssert(bool condition, string message);
    }
}