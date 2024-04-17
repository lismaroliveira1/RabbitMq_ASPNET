using Client.Domain.Entities;

namespace Client.Infrastructure.Interfaces;

public interface IPersonRepository : IBaseRepository<Person>{
    Task<List<Person>> GetAll();
}
