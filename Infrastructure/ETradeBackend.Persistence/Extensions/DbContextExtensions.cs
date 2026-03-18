using ETradeBackend.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Extensions;

public static class DbContextExtensions
{
    public static void UpdateBaseEntityTimestamps(this DbContext context)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            var hasCreatedAt = entry.Metadata.FindProperty(nameof(BaseEntity.CreatedAt)) is not null;
            var hasUpdatedAt = entry.Metadata.FindProperty(nameof(BaseEntity.UpdatedAt)) is not null;

            switch (entry.State)
            {
                case EntityState.Added:
                    if (hasCreatedAt)
                        entry.Entity.CreatedAt = now;

                    if (hasUpdatedAt)
                        entry.Property(nameof(BaseEntity.UpdatedAt)).IsModified = false;
                    break;

                case EntityState.Modified:
                    if (hasUpdatedAt)
                        entry.CurrentValues[nameof(BaseEntity.UpdatedAt)] = now;

                    if (hasCreatedAt)
                        entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    break;
            }
        }
    }
}