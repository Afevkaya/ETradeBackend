using ETradeBackend.Domain.Entities;
using ETradeBackend.Domain.Entities.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETradeBackend.Persistence.EntityTypeConfigurations.Baskets;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId).IsRequired();
        builder.HasMany(b => b.BasketItems).WithOne(bi => bi.Basket).HasForeignKey(bi => bi.BasketId);
        builder.HasOne(b => b.Order).WithOne(c => c.Basket).HasForeignKey<Order>(b => b.Id);
    }
}