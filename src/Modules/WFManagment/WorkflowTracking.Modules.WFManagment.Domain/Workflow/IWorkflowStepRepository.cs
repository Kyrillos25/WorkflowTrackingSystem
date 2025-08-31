namespace WorkflowTracking.Modules.WFManagment.Domain.Workflow;
public interface IWorkflowStepRepository
{
    Task Insert(Guid workflowId, List<WorkflowStep> workflowSteps, CancellationToken cancellationToken);
    Task UpdateAsync(Guid workflowId, List<WorkflowStep> workflowSteps, CancellationToken cancellationToken = default);
}
