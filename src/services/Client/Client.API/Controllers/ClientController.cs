using Client.Service.MessageBroker.Producer;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers;
[ApiController]
public class ClientController : ControllerBase {

    private readonly RpcClient _client;

    public ClientController(RpcClient client)
    {
        _client = client;
    }

    [HttpGet]
    [Route("/api/v1/send/{id}")]
     public async Task<IActionResult> SignIn(long id)
    {
        try
        {   Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("Id",  id.ToString());
            var user = _client.Call<RequestMessage>(new RequestMessage { Method = "GetUser", Payload = payload });
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

public record RequestMessage
{
    public string Method { get; set;}
    public Dictionary<string, string> Payload {get; set;}
}