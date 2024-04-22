using Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers;
[ApiController]
public class ClientController : ControllerBase {

    private readonly IPersonService _personServices;

    public ClientController(IPersonService personServices)
    {
        _personServices = personServices;
    }

    [HttpGet]
    [Route("/api/v1/send/{id}")]
     public async Task<IActionResult> SignIn(long id)
    {
        try
        {   
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