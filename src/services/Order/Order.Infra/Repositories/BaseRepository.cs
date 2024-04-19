using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infra.Contexts;
using Order.Infra.Interfaces;

namespace Order.Infra.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly OrderContext _context;
    public BaseRepository(OrderContext context) {
        _context = context;
    }
    public async Task<T> Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();
        return obj;
    }

    async Task<T> IBaseRepository<T>.Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return obj;
    }
}