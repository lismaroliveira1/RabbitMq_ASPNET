
using Serilog;
using ServerLogger.Infra.Interfaces;
namespace ServerLogger.Infra.Factories;


public class LoggerFactory : ILoggerFactory
{
    public void LogDebug(string message)
    {
        Log.Debug(message);
    }

    public void LogError(string message)
    {
        Log.Error(message);
    }

    public void LogFatal(string message)
    {
        Log.Fatal(message);
    }

    public void LogInfo(string message)
    {
        Log.Information(message);
    }

    public void LogWarning(string message)
    {
        Log.Warning(message);
    }
}