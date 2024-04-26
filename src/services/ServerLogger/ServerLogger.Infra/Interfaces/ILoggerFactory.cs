namespace ServerLogger.Infra.Interfaces;

public interface ILoggerFactory {
    public void LogError(string message);
    public void LogInfo(string message);
    public void LogFatal(string message);
    public void LogDebug(string message);
    public void LogWarning(string message);
}