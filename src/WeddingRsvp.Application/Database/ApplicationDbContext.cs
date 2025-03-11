using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Empty Constructot.
    }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
