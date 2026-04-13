using ETradeBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETradeBackend.Persistence.EntityTypeConfigurations;

public class ProductProductImageFilesConfiguration : IEntityTypeConfiguration<ProductProductImageFiles>
{
    public void Configure(EntityTypeBuilder<ProductProductImageFiles> builder)
    {
        builder.ToTable("productproductimagefiles");

        builder.HasKey(x => new { x.ProductId, x.ProductImageFileId });

        builder.Property(x => x.ProductId).HasColumnName("productsid");
        builder.Property(x => x.ProductImageFileId).HasColumnName("productimagefilesid");

        builder.Property(x => x.IsShowcase).HasColumnName("isshowcase").HasDefaultValue(false);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductProductImageFiles)
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.ProductImageFile)
            .WithMany(x => x.ProductProductImageFiles)
            .HasForeignKey(x => x.ProductImageFileId);
    }
}