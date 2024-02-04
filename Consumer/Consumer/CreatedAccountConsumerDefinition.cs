namespace Consumers;

public class CreatedAccountConsumerDefinition : ConsumerDefinition<CreatedAccountConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<CreatedAccountConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        endpointConfigurator.UseEntityFrameworkOutbox<ConsumerContext>(context);
    }
}
