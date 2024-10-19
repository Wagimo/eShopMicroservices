using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Ordering.Infrastructure.Data.Intercerptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges ( DbContextEventData eventData, InterceptionResult<int> result )
    {
        UpdateEntities ( eventData.Context );
        return base.SavingChanges ( eventData, result );
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync ( DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default )
    {
        UpdateEntities ( eventData.Context );
        return base.SavingChangesAsync ( eventData, result, cancellationToken );
    }

    public static void UpdateEntities ( DbContext? context )
    {
        if (context is null) return;

        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<IEntity> ())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "System";
                entry.Entity.CreatedAt = now;
            }

            if (entry.State == EntityState.Modified || entry.State == EntityState.Added || entry.HasChangedOwnedEntities ())
            {
                entry.Entity.LastModifiedBy = "System";
                entry.Entity.LastModified = now;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities ( this EntityEntry entry ) =>
        entry.References.Any ( x =>
        x.TargetEntry is not null &&
        x.TargetEntry.State is EntityState.Added or EntityState.Modified &&
        x.TargetEntry.Metadata.IsOwned ()
        );

}
