using Microsoft.EntityFrameworkCore;
using SlotEngine.Domain.Entities;

namespace SlotEngine.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<GameConfigVersion> GameConfigVersions => Set<GameConfigVersion>();
    public DbSet<SpinRecord> SpinRecords => Set<SpinRecord>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
