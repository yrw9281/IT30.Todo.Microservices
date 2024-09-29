using Account.Domain.Aggregates;
using Account.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure;

public class AccountConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 設定資料表名稱
        builder.ToTable("tb_user");

        // 設定主鍵
        builder.HasKey(u => u.Id);

        // 設定 Property Mapping
        // 轉換 UserId -> Guid
        // 需要建立新的 Overloading Method: UserId.Create(Value)
        builder.Property(u => u.Id)
               .HasColumnName("id")
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => UserId.Create(value))
               .IsRequired();

        // 設定 Property Mapping
        builder.Property(u => u.FirstName)
               .HasColumnName("first_name")
               .HasMaxLength(100)
               .IsRequired();

        // 設定 Property Mapping
        builder.Property(u => u.LastName)
               .HasColumnName("last_name")
               .HasMaxLength(100)
               .IsRequired();

        // 設定 Property Mapping
        builder.Property(u => u.Email)
               .HasColumnName("email")
               .HasMaxLength(255)
               .IsRequired();
        // Email 有 UNIQUE CONSTRAINT 
        builder.HasIndex(u => u.Email)
               .IsUnique();

        // 設定 Property Mapping
        // 這邊需要實作將 Password 加密成 PasswordHash
        builder.Property(u => u.PasswordHash)
               .HasColumnName("password_hash")
               .HasMaxLength(255)
               .IsRequired();

        // 設定 Property Mapping
        builder.Property(u => u.CreatedDateTime)
               .HasColumnName("created_date_time")
               .IsRequired();

        // 設定 Property Mapping
        builder.Property(u => u.UpdatedDateTime)
               .HasColumnName("updated_date_time")
               .IsRequired();
    }
}
