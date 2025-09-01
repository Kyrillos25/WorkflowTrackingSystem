namespace WorkflowTracking.Common.Application.EventBus;
public interface IEventBus
{
    Task<TResponse> PublishAsync<TRequest, TResponse>(
        TRequest integrationEvent,
        CancellationToken cancellationToken = default)
        where TRequest : class, IIntegrationEvent
        where TResponse : class;
}

