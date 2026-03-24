using ETradeBackend.Domain.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETradeBackend.Persistence.EntityTypeConfigurations;

public class InvoiceFileConfiguration : IEntityTypeConfiguration<InvoiceFile>
{
    public void Configure(EntityTypeBuilder<InvoiceFile> builder)
    {
        builder.Property(ifile => ifile.Price).IsRequired().HasColumnType("decimal(18,2)");
    }
}