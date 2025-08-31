using WorkflowTracking.Modules.WFManagment.Domain.Workflow;
using WorkflowTracking.Modules.WFManagment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;
internal sealed class WorkflowRepository(WorkflowsDbContext context) : IWorkflowRepository
{
    public void Insert(Workflow workflow)
    {
        context.Workflows.Add(workflow);
    }

    public async Task<Workflow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Workflows
            .Include(w => w.Steps)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public async Task<List<Workflow>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await context.Workflows
            .Include(w => w.Steps).ToListAsync(cancellationToken);
    }
}
