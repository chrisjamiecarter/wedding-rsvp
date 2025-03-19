using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Empty constructor.
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventFoodOption> EventFoodOptions { get; set; }
    public DbSet<FoodOption> FoodOptions { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
