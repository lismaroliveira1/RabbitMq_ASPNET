namespace MessageBroker.Core.Model;

public class Request {
    public string Method {get; set;}
    public dynamic Payload {get; set;}
}