using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFProcessor.Domain.Processor;
public sealed class ProcessStepExecution : Entity
{
    private ProcessStepExecution()
    {
    }
    public Guid Id { get; private set; }
    public string StepName { get; set; } = null!;
    public string PerformedBy { get; set; } = null!;
    public string Action { get; set; } = null!;
    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Completed";
    public Guid ProcessId { get; set; }
    public Process Process { get; set; }

    public static ProcessStepExecution Create(string stepName, string performedBy, string action, string status = "Completed")
    {
        var processStepExecution = new ProcessStepExecution
        {
            Id = Guid.NewGuid(),
            StepName = stepName,
            PerformedBy = performedBy,
            Action = action,
            Status = status
        };
        return processStepExecution;
    }
}
