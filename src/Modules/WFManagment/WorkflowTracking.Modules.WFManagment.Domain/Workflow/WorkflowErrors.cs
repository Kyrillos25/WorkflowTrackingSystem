using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFManagment.Domain.Workflow;
public static class WorkflowErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Workflows.NotFound", $"Workflow with the identifier {id} not found");

}
