namespace WorkflowTracking.Modules.WFProcessor.Domain.Processor;
public interface IProcessRepository
{
    void Insert(Process process);
    Task<Process?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Process?> GetByWorkFlowIdAsync(Guid workFlowId, CancellationToken cancellationToken = default);
    Task<List<Process>> GetAsync(CancellationToken cancellationToken = default);
}
