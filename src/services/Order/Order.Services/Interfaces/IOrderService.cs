using Order.Services.DTOs;

namespace Order.Services.Interfaces;

public interface IOrderService {
    Task<OrderDto> Create(OrderDto orderDto);
    Task<OrderDto> Update(OrderDto orderDto);
    Task Remove(long id);
    Task<OrderDto> Get(long id);
    Task<List<OrderDto>> GetAll();
} 