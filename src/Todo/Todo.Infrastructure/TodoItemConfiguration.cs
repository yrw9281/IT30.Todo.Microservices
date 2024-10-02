using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Aggregates;
using Todo.Domain.ValueObjects;
using Todo.Domain.ValueObjects.Enums;

namespace Todo.Infrastructure;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("tb_todo_item");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TodoItemId.Create(value))
            .IsRequired();

        builder.Property(t => t.Content)
            .HasColumnName("content")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasConversion(
                v => v.State.ToString().ToLower(), // Enum 狀態轉字串
                v => new TodoItemStatus((TodoItemState)Enum.Parse(typeof(TodoItemState), v, true))) // 字串轉 Enum 狀態
            .IsRequired();

        builder.Property(t => t.ListId)
            .HasColumnName("list_id")
            .IsRequired();

        builder.Property(t => t.CreatedDateTime)
            .HasColumnName("created_date_time")
            .IsRequired();

        builder.Property(t => t.UpdatedDateTime)
            .HasColumnName("updated_date_time")
            .IsRequired();
    }
}
