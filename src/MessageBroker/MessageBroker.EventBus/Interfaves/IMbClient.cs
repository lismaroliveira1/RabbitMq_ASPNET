using MessageBroker.Core.Model;

namespace MessageBroker.EventBus.Interfaces;
public interface IMbClient {
    public Response? Call<T>(T obj);
    public void Close();
}