using Client.Domain.Entities;
using Client.Infrastructure.Context;
using Client.Infrastructure.Interfaces;

namespace Client.Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly PersonContext _context;
    public PersonRepository(PersonContext context) : base(context)
    {
        _context = context;
    }

    Task<Person> IPersonRepository.GetAll()
    {
        throw new NotImplementedException();
    }
}