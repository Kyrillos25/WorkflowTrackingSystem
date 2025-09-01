using MassTransit;
namespace WorkflowTracking.Common.Application.EventBus;
public abstract class IntegrationRequestHandler<TRequest, TResponse>
    : IConsumer<TRequest>
    where TRequest : class, IIntegrationEvent
    where TResponse : class
{
    public abstract Task<TResponse> Handle(TRequest integrationEvent, CancellationToken cancellationToken = default);

 
   public async Task Consume(ConsumeContext<TRequest> context)
    {
        TResponse response = await Handle(context.Message, context.CancellationToken);
        await context.RespondAsync(response);
    }
}

public abstract class IntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    public abstract Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);

    public Task Handle(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        Handle((TIntegrationEvent)integrationEvent, cancellationToken);
}
