using Common.Library.Seedwork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Library.Interceptors;

public class DomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _mediator;

    public DomainEventsInterceptor(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        PublishDomainEvents(eventData.Context).Wait();
        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
            return;

        var entities = dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();

        var events = entities
            .SelectMany(entry => entry.DomainEvents)
            .ToList();

        foreach (var entity in entities)
            entity.ClearDomainEvents();

        foreach (var domainEvent in events)
            await _mediator.Publish(domainEvent);
    }
}
