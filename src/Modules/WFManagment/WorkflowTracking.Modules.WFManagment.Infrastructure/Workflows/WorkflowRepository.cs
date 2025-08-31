using WorkflowTracking.Modules.WFManagment.Domain.Workflow;
using WorkflowTracking.Modules.WFManagment.Infrastructure.Database;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;
internal sealed class WorkflowRepository(WorkflowsDbContext context) : IWorkflowRepository
{
    public void Insert(Workflow workflow)
    {
        context.Workflows.Add(workflow);
    }
}
