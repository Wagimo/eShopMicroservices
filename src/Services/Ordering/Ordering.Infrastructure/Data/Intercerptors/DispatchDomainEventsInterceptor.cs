using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Intercerptors;

public class DispatchDomainEventsInterceptor ( IMediator mediator ) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges ( DbContextEventData eventData, InterceptionResult<int> result )
    {
        DispatchDomainEvents ( eventData.Context ).GetAwaiter ().GetResult ();
        return base.SavingChanges ( eventData, result );
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync ( DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default )
    {
        await DispatchDomainEvents ( eventData.Context );
        return await base.SavingChangesAsync ( eventData, result, cancellationToken );
    }

    private async Task DispatchDomainEvents ( DbContext? context )
    {
        if (context is null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate> ()
            .Where ( x => x.Entity.DomainEvents.Any () )
            .Select ( x => x.Entity );

        if (!aggregates.Any ()) return;

        var domainEvents = aggregates
            .SelectMany ( x => x.DomainEvents )
            .ToList ();

        //Limpiamos los eventos de dominio para evitar que se envíen más eventos de dominio en la misma transacción
        aggregates.ToList ()
            .ForEach ( x => x.ClearDomainEvents () );

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish ( domainEvent );
        }
    }
}
