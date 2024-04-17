using Client.Domain.Entities;

namespace Client.Infrastructure.Interfaces;

public interface IPersonRepository <T> where T : Base
{
    Task<T> GetAll();
}
