namespace Consumers;

public class CreatedAccountConsumer(
    ILogger<CreatedAccountConsumer> logger, 
    ConsumerContext dbContext) : IConsumer<CreatedAccount>
{
    public async Task Consume(ConsumeContext<CreatedAccount> context)
    {
        logger.LogInformation("");

        var account = new Account{
            Id = context.Message.AccountId,
            AccountNumber = context.Message.AccountNumber,
            CreatedAt = context.Message.CreatedAt,
            ConsumedAt = DateTime.Now,
        };

        dbContext.Add(account);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Consumer 1: Successfully created account: {@account}", account);

        return;
    }
}
