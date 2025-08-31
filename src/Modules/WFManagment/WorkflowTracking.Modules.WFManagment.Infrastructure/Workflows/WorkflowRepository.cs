using WorkflowTracking.Modules.WFManagment.Domain.Workflow;
using WorkflowTracking.Modules.WFManagment.Infrastructure.Database;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;
internal sealed class WorkflowRepository(WorkflowsDbContext context) : IWorkflowRepository
{
    public void Insert(Workflow workflow)
    {
        foreach (WorkflowStep role in workflow.Steps)
        {
            context.Attach(role);
        }

        context.Workflows.Add(workflow);
    }
}
