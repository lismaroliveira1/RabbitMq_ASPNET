using Client.Domain.Entity;

namespace Client.Services.MessageBroker.Model
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public Person Payload {get; set;}
    }
}
