using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infra.Contexts;
using Order.Infra.Interfaces;

namespace Order.Infra.Repositories;

public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
{
    private readonly OrderContext _context;
    public OrderRepository(OrderContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<OrderEntity>> GetAll()
    {
        var orders = _context.Orders.AsNoTracking().ToListAsync();
        return orders;
    }
}