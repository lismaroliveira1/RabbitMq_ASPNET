
namespace ServerLogger.Infra.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public dynamic Payload {get; set;}
    }
}
