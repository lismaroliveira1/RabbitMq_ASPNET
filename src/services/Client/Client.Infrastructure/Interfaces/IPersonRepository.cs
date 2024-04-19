using Client.Domain.Entity;

namespace Client.Infrastructure.Interfaces;

public interface IPersonRepository : IBaseRepository<Person>{
    Task<List<Person>> GetAll();
}
