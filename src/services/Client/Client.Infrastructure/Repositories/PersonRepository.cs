using Client.Domain.Entity;
using Client.Infrastructure.Context;
using Client.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly PersonContext _context;
    public PersonRepository(PersonContext context) : base(context)
    {
        _context = context;
    }

    Task<List<Person>> IPersonRepository.GetAll()
    {
        return _context.Person.AsNoTracking().ToListAsync();
    }
}