using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFProcessor.Domain.Processor;
public sealed class Process : Entity
{
    private readonly List<ProcessStepExecution> _executions = [];

    private Process()
    {
    }
    public Guid Id { get; private set; }
    public Guid WorkflowId { get; set; }
    public string Initiator { get; set; } = null!;
    public string Status { get; set; } = "Active";
    public IReadOnlyCollection<ProcessStepExecution> Executions => _executions.ToList();
    public static Process Create(Guid workflowId, string initiator)
    {
        var process = new Process
        {
            Id = Guid.NewGuid(),
            WorkflowId = workflowId,
            Initiator = initiator
        };
        return process;
    }
    public void CompleteProcess()
    {
        Status = "Complete";
    }
}
