using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFProcessor.Domain.Processor;
public static class ProcessStepExecutionErrors
{
    public static Error NotFound(Guid id) =>
        Error.Problem("Processors.InvalidStep", $"Process with the identifier {id} doesn't have current step");
    public static Error InvalidStepName(string name) =>
        Error.Problem("Processors.InvalidStepName", $"Step with the name {name} is not valid");
    public static Error InvalidRole(string performedBy) =>
        Error.Problem("Processors.InvalidRole", $"Step with role {performedBy} is not valid");
}
