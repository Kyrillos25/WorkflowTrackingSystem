using Microsoft.EntityFrameworkCore;
using WorkflowTracking.Common.Infrastructure.Inbox;
using WorkflowTracking.Common.Infrastructure.Outbox;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;
using WorkflowTracking.Modules.WFProcessor.Infrastructure.Processors;

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Database;
public sealed class ProcessorsDbContext(DbContextOptions<ProcessorsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Process> Processes { get; set; }
    internal DbSet<ProcessStepExecution> ProcessStepExecutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Processors);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new ProcessConfiguration());
        modelBuilder.ApplyConfiguration(new ProcessStepExecutionConfiguration());
    }
}
