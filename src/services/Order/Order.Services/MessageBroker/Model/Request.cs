namespace Order.Services.MessageBroker.Model;
public record Request
{
    public string Method { get; set;}
    public Dictionary<string, string> Payload {get; set;}
}
