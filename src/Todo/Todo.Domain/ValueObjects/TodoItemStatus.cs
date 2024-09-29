using Common.Library.Seedwork;
using Todo.Domain.ValueObjects.Enums;

namespace Todo.Domain.ValueObjects;

public class TodoItemStatus : ValueObject
{
    public TodoItemState State { get; private set; }
    public string Color => GetColorByState(State); // 顏色依據狀態自動計算

    public TodoItemStatus(TodoItemState state)
    {
        State = state;
    }

    public static TodoItemStatus Default() => new (TodoItemState.Todo);

    private string GetColorByState(TodoItemState state)
    {
        return state switch
        {
            TodoItemState.Todo => "#FFFF00", // 黃色
            TodoItemState.Finished => "#008000", // 綠色
            TodoItemState.Removed => "#808080", // 灰色
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unknown state: {state}")
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }
}
