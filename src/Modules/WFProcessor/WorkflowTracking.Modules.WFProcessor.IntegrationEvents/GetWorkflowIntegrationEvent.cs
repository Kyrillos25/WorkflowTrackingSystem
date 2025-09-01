using WorkflowTracking.Common.Application.EventBus;

namespace WorkflowTracking.Modules.WFProcessor.IntegrationEvents;
public sealed class GetWorkflowIntegrationEvent : IntegrationEvent
{
    public GetWorkflowIntegrationEvent(Guid id, DateTime occurredOnUtc)
        : base(id, occurredOnUtc)
    {
    }
}
