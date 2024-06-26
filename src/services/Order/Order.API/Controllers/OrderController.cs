using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.Api.ViewModels;
using Order.API.Utilities;
using Order.API.ViewModels;
using Order.Core.Exceptions;
using Order.Services.DTOs;
using Order.Services.Interfaces;

namespace Order.API.Controllers;

[ApiController]
public class OrderController(IOrderService orderService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Route("/api/v1/orders/{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var orderDto = await orderService.Get(id);
            if (orderDto == null) return Ok(new ResultViewModel
            {
                Message = "Order not found",
                Success = true,
                Data = new { }
            });
            return Ok(new ResultViewModel
            {
                Message = "Order found successfully",
                Success = true,
                Data = orderDto
            });
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

    [HttpGet]
    [Route("/api/v1/orders")]
    public async Task<IActionResult> GetAll(long id)
    {
        try
        {
            var orders = await orderService.GetAll();
            return Ok(new ResultViewModel
            {
                Message = "Order found successfully",
                Success = true,
                Data = orders
            });
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

    [HttpPost]
    [Route("/api/v1/orders/create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderViewModel order)
    {
        try
        {
            var userDTO = mapper.Map<OrderDto>(order);
            var newOrder = await orderService.Create(userDTO);
            return Ok(new ResultViewModel
            {
                Message = "Order created successfully",
                Success = true,
                Data = newOrder
            });
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

    [HttpPut]
    [Route("/api/v1/order/update")]
    public async Task<IActionResult> Update([FromBody] UpdateOrderViewModel order)
    {
        try
        {
            var orderDto = mapper.Map<OrderDto>(order);
            var orderUpdated = await orderService.Update(orderDto);
            return Ok(new ResultViewModel
            {
                Message = "Order updated successfully",
                Success = true,
                Data = orderUpdated,
            });
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

    [HttpDelete]
    [Route("api/v1/order/delete/{id}")]
    public async Task<IActionResult> Delete(long id) {
        try {
            await orderService.Remove(id);
            return Ok(new ResultViewModel{
                Message = "Order deleted successfully",
                Success = true,
                Data = new {}
            });
        }catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors!));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Message));
        }
    }
}
