using Microsoft.EntityFrameworkCore;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;
using WorkflowTracking.Modules.WFProcessor.Infrastructure.Database;

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Processors;
internal sealed class ProcessRepository(ProcessorsDbContext context) : IProcessRepository
{
    public async Task<List<Process>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await context.Processes.ToListAsync(cancellationToken);
    }

    public async Task<Process?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Processes.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Process?> GetByWorkFlowIdAsync(Guid workFlowId, CancellationToken cancellationToken = default)
    {
        return await context.Processes.FirstOrDefaultAsync(p => p.WorkflowId == workFlowId, cancellationToken);
    }

    public void Insert(Process process)
    {
        context.Processes.Add(process);
    }
}
