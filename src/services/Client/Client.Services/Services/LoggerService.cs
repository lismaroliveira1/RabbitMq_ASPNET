using Client.Services.Interfaces;
using MessageBroker.EventBus.Core;
using MessageBroker.EventBus.Producer;
using Newtonsoft.Json;

namespace Client.Services.Services;

public class LoggerService : ILoggerService
{
    private readonly ProducerEvent _producerEvent;

    public LoggerService(ProducerEvent producerEvent)
    {
        _producerEvent = producerEvent;
    }

    public void LogDebug(string message)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("message", message);
        data.Add("type", "Debug");
         _producerEvent.Publish($"{EventBusConstants.DirectQueue}", JsonConvert.SerializeObject(data));
    }

    public void LogError(string message)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("message", message);
        data.Add("type", "Error");
         _producerEvent.Publish($"{EventBusConstants.DirectQueue}", JsonConvert.SerializeObject(data));
        throw new NotImplementedException();
    }

    public void LogFatal(string message)
    {
         Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("message", message);
        data.Add("type", "Fatal");
         _producerEvent.Publish($"{EventBusConstants.DirectQueue}", JsonConvert.SerializeObject(data));
        throw new NotImplementedException();
    }

    public void LogInfo(string message)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("message", message);
        data.Add("type", "Info");
         _producerEvent.Publish($"{EventBusConstants.DirectQueue}", JsonConvert.SerializeObject(data));
        throw new NotImplementedException();
    }

    public void LogWarning(string message)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("message", message);
        data.Add("type", "Warning");
        _producerEvent.Publish($"{EventBusConstants.DirectQueue}", JsonConvert.SerializeObject(data));
        throw new NotImplementedException();
    }
}