using Common.Library.Seedwork;

namespace Todo.Domain.ValueObjects;

public class TodoListId : ValueObject
{
    public Guid Value { get; private set; }
    
    public TodoListId(Guid value)
    {
        Value = value;
    }

    public static TodoListId Create()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
