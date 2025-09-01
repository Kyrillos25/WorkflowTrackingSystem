using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFProcessor.Domain.Processor;
public static class ProcessErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Processors.NotFound", $"Process with the identifier {id} not found");
}
