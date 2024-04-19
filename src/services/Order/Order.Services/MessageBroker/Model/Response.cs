using Order.Domain.Entities;

namespace Order.Services.MessageBroker.Model
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public OrderEntity Payload {get; set;}
    }
}
