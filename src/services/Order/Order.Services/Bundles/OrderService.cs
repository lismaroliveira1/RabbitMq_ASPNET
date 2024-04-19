using AutoMapper;
using Order.Core.Exceptions;
using Order.Domain.Entities;
using Order.Infra.Interfaces;
using Order.Services.DTOs;
using Order.Services.Interfaces;
using Order.Services.MessageBroker.Producer;

namespace Order.Services.Bundles;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    public OrderService(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    async Task<OrderDto> IOrderService.Create(OrderDto orderDto)
    {
        var _hasOrder = await _orderRepository.Get(orderDto.Id);
        if (_hasOrder != null) throw new DomainException("This order is already registered");
        var order = _mapper.Map<OrderEntity>(orderDto);
        order.Validate();
        var newOrder = await _orderRepository.Create(order);
        return _mapper.Map<OrderDto>(newOrder);
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