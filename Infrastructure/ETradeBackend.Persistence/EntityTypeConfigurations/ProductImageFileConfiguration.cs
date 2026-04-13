using ETradeBackend.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETradeBackend.Persistence.EntityTypeConfigurations;

public class ProductImageFileConfiguration : IEntityTypeConfiguration<ProductImageFile>
{
    public void Configure(EntityTypeBuilder<ProductImageFile> builder)
    {
        
    }
}