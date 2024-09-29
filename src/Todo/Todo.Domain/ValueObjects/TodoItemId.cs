using Common.Library.Seedwork;

namespace Todo.Domain.ValueObjects;

public class TodoItemId : ValueObject
{
    public Guid Value { get; private set; }
    
    public TodoItemId(Guid value)
    {
        Value = value;
    }

    public static TodoItemId Create()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
