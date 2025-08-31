namespace WorkflowTracking.Modules.WFManagment.Domain.Workflow;
public interface IWorkflowRepository
{
    void Insert(Workflow workflow);
    Task<Workflow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Workflow>> GetAsync(CancellationToken cancellationToken = default);
}
