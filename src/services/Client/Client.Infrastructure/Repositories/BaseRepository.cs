using Client.Domain.Entity;
using Client.Infrastructure.Context;
using Client.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly PersonContext _context;

    public BaseRepository(PersonContext context) {
        _context = context;
    }

    public async Task Delete(long id)
    {
        var motorcycles = await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).ToListAsync();
        var user = motorcycles.FirstOrDefault();
        if (user != null) {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    async Task<T> IBaseRepository<T>.Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();
        return obj;
    }

    async Task<T> IBaseRepository<T>.Get(long id)
    {
        var obj = await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).ToListAsync();
        return obj.FirstOrDefault()!;
    }

    async Task<T> IBaseRepository<T>.Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return obj;
    }
}