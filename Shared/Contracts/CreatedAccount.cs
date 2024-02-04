namespace Contracts;

public record CreatedAccount
{
    public Guid AccountId { get; init; }
    public string AccountNumber { get; init; } = null!;
    public DateTime CreatedAt { get; set; }
}

