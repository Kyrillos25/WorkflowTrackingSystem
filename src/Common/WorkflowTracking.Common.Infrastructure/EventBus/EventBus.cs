using MassTransit;
using WorkflowTracking.Common.Application.EventBus;

namespace WorkflowTracking.Common.Infrastructure.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task<TResponse> PublishAsync<TRequest, TResponse>(
        TRequest integrationEvent,
        CancellationToken cancellationToken = default)
        where TRequest : class, IIntegrationEvent
        where TResponse : class
    {
        IRequestClient<TRequest> client = bus.CreateRequestClient<TRequest>();

        return (await client.GetResponse<TResponse>(
            integrationEvent,
            cancellationToken
        )).Message;
    }
}


