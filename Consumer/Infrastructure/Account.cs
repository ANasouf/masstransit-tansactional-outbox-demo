using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AccountNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ConsumedAt { get; set; }
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(p => p.AccountNumber).HasMaxLength(20);

        builder.HasIndex(p => p.AccountNumber).IsUnique();
    }
}