using ETradeBackend.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETradeBackend.Persistence.EntityTypeConfigurations.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Description).IsRequired().HasMaxLength(255);
        builder.Property(o => o.Address).IsRequired().HasMaxLength(255);
        builder.Property(o => o.Code)
            .IsRequired()
            .HasMaxLength(8)
            .IsUnicode(false);

        builder.HasIndex(o => o.Code).IsUnique();
    }
}