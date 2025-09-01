using Microsoft.EntityFrameworkCore;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;
using WorkflowTracking.Modules.WFProcessor.Infrastructure.Database;

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Processors;
internal sealed class ProcessStepExecutionRepository(ProcessorsDbContext context) : IProcessStepExecutionRepository
{
    public async Task<List<ProcessStepExecution>> GetCompletedSteps(Guid processId, CancellationToken cancellationToken = default)
    {
        return await context.ProcessStepExecutions.Where(x => x.ProcessId == processId && x.Status == "Completed").OrderByDescending(x => x.PerformedAt)
            .ToListAsync(cancellationToken);
    }

    public void Insert(ProcessStepExecution processStepExecution)
    {
        context.ProcessStepExecutions.Add(processStepExecution);
    }
}
