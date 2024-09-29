using Common.Library.Seedwork;

namespace Account.Domain.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; private set; }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
