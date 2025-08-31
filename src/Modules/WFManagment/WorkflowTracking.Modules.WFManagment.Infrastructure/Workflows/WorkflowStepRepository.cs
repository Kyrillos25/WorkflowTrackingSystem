using Microsoft.EntityFrameworkCore;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;
using WorkflowTracking.Modules.WFManagment.Infrastructure.Database;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;
internal sealed class WorkflowStepRepository(WorkflowsDbContext context) : IWorkflowStepRepository
{
    public async Task Insert(Guid workflowId, List<WorkflowStep> workflowSteps, CancellationToken cancellationToken)
    {
        workflowSteps.ForEach(x => x.WorkflowId = workflowId);
        await context.WorkflowSteps.AddRangeAsync(workflowSteps, cancellationToken);
    }

    public async Task UpdateAsync(Guid workflowId, List<WorkflowStep> workflowSteps, CancellationToken cancellationToken = default)
    {
        await context.WorkflowSteps.Where(ws => ws.WorkflowId == workflowId).ExecuteDeleteAsync(cancellationToken);
        workflowSteps.ForEach(x => x.WorkflowId = workflowId);
        await context.WorkflowSteps.AddRangeAsync(workflowSteps, cancellationToken);
    }
}
