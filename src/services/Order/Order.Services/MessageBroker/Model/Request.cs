namespace Order.Infrastructure.Messages;
public record Request
{
    public string Method { get; set;}
    public Dictionary<string, string> Payload {get; set;}
}
