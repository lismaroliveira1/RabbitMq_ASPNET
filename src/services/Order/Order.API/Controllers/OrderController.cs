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
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("/api/v1/orders/{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var orderDto = await _orderService.Get(id);
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

    [HttpPost]
    [Route("/api/v1/orders/create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderViewModel order)
    {
        try
        {
            var userDTO = _mapper.Map<OrderDto>(order);
            var newOrder = await _orderService.Create(userDTO);
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

    public async Task<IActionResult> Update([FromBody] UpdateOrderViewModel order)
    {
        try
        {
            var orderDto = _mapper.Map<OrderDto>(order);
            var orderUpdated = await _orderService.Update(orderDto);
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
}

public class UpdateOrderViewModel
{
    public long Id { get; set; }
    public string OrderStatus { get; set; }
    public long Person { get; set; }
}