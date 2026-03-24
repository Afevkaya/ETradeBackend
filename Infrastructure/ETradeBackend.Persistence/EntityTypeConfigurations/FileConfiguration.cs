using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = ETradeBackend.Domain.Entities.Files.File;

namespace ETradeBackend.Persistence.EntityTypeConfigurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Path).IsRequired().HasMaxLength(500);
    }
}