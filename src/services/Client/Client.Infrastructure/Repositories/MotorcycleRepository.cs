using Client.Domain.Entities;
using Client.Infrastructure.Context;
using Client.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Motorcycle.Infrastructure.Repositories;

public class MotorcycleRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly PersonContext _context;
    public MotorcycleRepository(PersonContext context) : base(context)
    {
        _context = context;
    }

}