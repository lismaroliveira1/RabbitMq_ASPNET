using Client.Domain.Entities;
using Client.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Client.Infrastructure.Context;

public class PersonContext :DbContext
{
    public IConfiguration _configuration { get; }

    public PersonContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PersonContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Person> Partners { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PersonMap());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}
