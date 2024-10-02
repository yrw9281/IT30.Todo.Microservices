using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Aggregates;
using Todo.Domain.ValueObjects;
using Todo.Domain.ValueObjects.Enums;

namespace Todo.Infrastructure;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.ToTable("tb_todo_list");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TodoListId.Create(value))
            .IsRequired();

        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasConversion(
                s => s.ToString().ToLower(), // Enum 轉字串
                s => (TodoListStatus)Enum.Parse(typeof(TodoListStatus), s, true)) // 字串轉 Enum
            .IsRequired();

        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(t => t.CreatedDateTime)
            .HasColumnName("created_date_time")
            .IsRequired();

        builder.Property(t => t.UpdatedDateTime)
            .HasColumnName("updated_date_time")
            .IsRequired();

        builder.Ignore(t => t.TodoItemIds);
    }
}
