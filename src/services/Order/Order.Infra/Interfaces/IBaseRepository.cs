
using Order.Domain.Entities;

namespace Order.Infra.Interfaces;

public interface IBaseRepository<T> where T : Base {
    Task<T> Get(long id);
    Task<T> Update(T obj);
    Task<T> Create(T obj);
    Task Delete(long id);
}