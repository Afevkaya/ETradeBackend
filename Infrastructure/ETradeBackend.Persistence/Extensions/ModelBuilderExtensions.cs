using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyLowerCaseNamingConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToLowerInvariant());
            entity.SetSchema(entity.GetSchema()?.ToLowerInvariant());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLowerInvariant());
            }
        }
    }
}