
namespace Client.Services.Interfaces;
public interface ILoggerService {
    public void LogError(string message);
    public void LogInfo(string message);
    public void LogFatal(string message);
    public void LogDebug(string message);
    public void LogWarning(string message);
}