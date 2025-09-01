using WorkflowTracking.Common.Application.EventBus;

namespace WorkflowTracking.Modules.WFProcessor.IntegrationEvents;
public sealed class GetWorkflowByIdIntegrationEvent : IntegrationEvent
{
    public GetWorkflowByIdIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid workflowId)
        : base(id, occurredOnUtc)
    {
        WorkflowId = workflowId;
    }
    public Guid WorkflowId { get; init; }
}
