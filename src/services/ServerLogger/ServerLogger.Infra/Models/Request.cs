namespace ServerLogger.Infra.Models;
public record Request
{
    public string Method { get; set;}
    public Dictionary<string, string> Payload {get; set;}
}
