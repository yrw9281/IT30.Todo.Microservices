namespace Common.Library.Seedwork;

public abstract class Entity<TId> : IHasDomainEvents where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public required TId Id { get; init; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}