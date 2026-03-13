using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Contexts;

public class ETradeDbContext(DbContextOptions<ETradeDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ETradeDbContext).Assembly);
        modelBuilder.ApplyLowerCaseNamingConvention();
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        this.UpdateBaseEntityTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }
}