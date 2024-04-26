using AutoMapper;
using MessageBroker.Core.Model;
using MessageBroker.EventBus.Interfaces;
using Order.Core.Exceptions;
using Order.Domain.Entities;
using Order.Infra.Interfaces;
using Order.Services.DTOs;
using Order.Services.Interfaces;

namespace Order.Services.Bundles;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IMbClient _mbClient;
    public OrderService(IMapper mapper, IOrderRepository orderRepository, IMbClient mbClient)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _mbClient = mbClient;
    }

    async Task<OrderDto> IOrderService.Create(OrderDto orderDto)
    {
        var user = GetUser(orderDto.Person);
        var _hasOrder = await _orderRepository.Get(orderDto.Id);
        if (_hasOrder != null) throw new DomainException("This order is already registered");
        var order = _mapper.Map<OrderEntity>(orderDto);
        order.Validate();
        var newOrder = await _orderRepository.Create(order);
        return _mapper.Map<OrderDto>(newOrder);
    }

    internal dynamic GetUser(long userId)
    {
        var user = _mbClient.Call<Request>(new Request
        {
            Method = "GetUser",
            Payload = new { Id = userId }
        });
        return user!.Payload;
    }

    async Task<OrderDto> IOrderService.Get(long id)
    {
        var OrderDto = await _orderRepository.Get(id);
        return _mapper.Map<OrderDto>(OrderDto);
    }

    async Task<List<OrderDto>> IOrderService.GetAll()
    {
        var orders = await _orderRepository.GetAll();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    async Task IOrderService.Remove(long id)
    {
        await _orderRepository.Delete(id);
    }

    async Task<OrderDto> IOrderService.Update(OrderDto orderDto)
    {
        var _hasOrder = await _orderRepository.Get(orderDto.Id);
        if (_hasOrder == null) throw new DomainException("Order not found");
        var order = _mapper.Map<OrderEntity>(orderDto);
        order.Validate();
        var newOrder = await _orderRepository.Update(order);
        return _mapper.Map<OrderDto>(newOrder);
    }
}