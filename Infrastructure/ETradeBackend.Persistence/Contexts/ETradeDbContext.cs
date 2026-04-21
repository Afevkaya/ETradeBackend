using ETradeBackend.Domain.Entities.Baskets;
using ETradeBackend.Domain.Entities.CompletedOrders;
using ETradeBackend.Domain.Entities.Files;
using ETradeBackend.Domain.Entities.Identities;
using ETradeBackend.Domain.Entities.Orders;
using ETradeBackend.Domain.Entities.Products;
using ETradeBackend.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Contexts;

public class ETradeDbContext(DbContextOptions<ETradeDbContext> options) : IdentityDbContext<AppUser,AppRole,Guid>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Domain.Entities.Files.File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<CompletedOrder> CompletedOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ETradeDbContext).Assembly);
        modelBuilder.ApplyLowerCaseNamingConvention();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        this.UpdateBaseEntityTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }
}