using Microsoft.AspNetCore.Mvc;
using Order.API.Utilities;
using Order.API.ViewModels;
using Order.Core.Exceptions;
using Order.Services.Interfaces;

namespace Order.API.Controllers;

[ApiController]
public class OrderController : ControllerBase {
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("/api/v1/orders/{id}")]
    public async Task<IActionResult> Get(long id) {
        try
        {
            var orderDto = await _orderService.Get(id);
            if(orderDto == null) return Ok(new ResultViewModel{
                Message = "Order not found",
                Success = true,
                Data = new {}
            }); 
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!)); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }
}