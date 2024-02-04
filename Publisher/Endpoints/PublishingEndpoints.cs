namespace Endpoints;

public static class PublishingEndpoints
{
    public static RouteGroupBuilder MapPublishingGroup(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/accounts");

        group.MapGet("/", async ([AsParameters] PublisherServices services) =>
        {
            var accounts = await services.Context.Accounts.ToListAsync();

            return TypedResults.Ok(accounts);
        });

        group.MapPost("/", async ([AsParameters] PublisherServices services, [FromBody] AccountModel account) =>
        {
            services.Logger.LogInformation("Started account creating workflow");

            var newAccount = new Account
            {
                AccountNumber = account.AccountNumber,
                CreatedAt = DateTime.Now
            };

            services.Context.Add(newAccount);

            await services.PublishEndpoint.Publish(
            new CreatedAccount
            {
                AccountId = newAccount.Id,
                AccountNumber = account.AccountNumber,
                CreatedAt = newAccount.CreatedAt
            });

            await services.Context.SaveChangesAsync();

            services.Logger.LogInformation(message: "Account: {@newAccount} created successfully", newAccount);

            return TypedResults.Created($"/api/accounts/{newAccount.Id}");
        });

        return group;
    }
}
