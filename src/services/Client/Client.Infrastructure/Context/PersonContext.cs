using Client.Domain.Entities;
using Client.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Client.Infrastructure.Context;

public class PersonContext :DbContext
{
    public PersonContext() {}
    public PersonContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Person> Partners { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PersonMap());
    }

    public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Ignore the CreatedTime updates on Modified entities. 
                    entry.Property("CreatedAt").IsModified = false;
                }
                // Always set UpdatedAt. Assuming all entities having CreatedAt property
                // Also have UpdatedAt
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        optionsBuilder.UseNpgsql(connectionString);
    }
}
