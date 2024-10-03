namespace Common.Library.Seedwork;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void AddDomainEvent(IDomainEvent domainEvent);
    public void ClearDomainEvents();
}
