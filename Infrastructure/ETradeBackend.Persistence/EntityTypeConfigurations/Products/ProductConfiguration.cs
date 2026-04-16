using ETradeBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETradeBackend.Persistence.EntityTypeConfigurations.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Stock).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)").HasDefaultValue(0);
            
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Product_Stock_Min", "\"stock\" >= 0");
                t.HasCheckConstraint("CK_Product_Price_Min", "\"price\" >= 0");
            });
    }
}