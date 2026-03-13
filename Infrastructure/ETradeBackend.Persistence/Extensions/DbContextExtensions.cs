using ETradeBackend.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Extensions;

public static class DbContextExtensions
{
    public static void UpdateBaseEntityTimestamps(this DbContext context)
    {
        var entries = context.ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Property(e=>e.UpdatedAt).IsModified = false;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Property(e=>e.CreatedAt).IsModified = false;
                    break;   
            }
        }

    }
}