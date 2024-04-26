using Client.Domain.Entity;

namespace Client.Infrastructure.Interfaces;

public interface IPersonRepository : IBaseRepository<PersonEntity>{
    Task<List<PersonEntity>> GetAll();
}
