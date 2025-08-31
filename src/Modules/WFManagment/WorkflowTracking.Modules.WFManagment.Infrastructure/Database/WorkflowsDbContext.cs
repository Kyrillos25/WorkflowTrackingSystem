using Microsoft.EntityFrameworkCore;
using WorkflowTracking.Common.Infrastructure.Inbox;
using WorkflowTracking.Common.Infrastructure.Outbox;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;
using WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Database;
public sealed class WorkflowsDbContext(DbContextOptions<WorkflowsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Workflow> Workflows { get; set; }
    internal DbSet<WorkflowStep> WorkflowSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Workflows);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new WorfkflowConfiguration());
        modelBuilder.ApplyConfiguration(new WorkflowStepConfiguration());
    }
}

