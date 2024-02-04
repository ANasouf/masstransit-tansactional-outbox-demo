namespace Endpoints;

public class PublisherServices(
    PublisherContext context,
    ILogger<PublisherServices> logger,
    IPublishEndpoint publishEndpoint)
{
    public PublisherContext Context { get; } = context;
    public ILogger<PublisherServices> Logger { get; } = logger;
    public IPublishEndpoint PublishEndpoint { get; } = publishEndpoint;
}
