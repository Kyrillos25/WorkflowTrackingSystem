namespace WorkflowTracking.Modules.WFProcessor.Domain.Processor;
public interface IProcessStepExecutionRepository
{
    void Insert(ProcessStepExecution processStepExecution);
    Task<List<ProcessStepExecution>> GetCompletedSteps(Guid processId, CancellationToken cancellationToken = default);
}
