using Client.Domain.Entity;

namespace Client.Infrastructure.Interfaces;
public interface IBaseRepository<T> where T : Base {
    Task<T> Get(long id);
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task Delete(long id);
}