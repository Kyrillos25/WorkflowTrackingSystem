using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;
internal sealed class WorkflowStepConfiguration : IEntityTypeConfiguration<WorkflowStep>
{
    public void Configure(EntityTypeBuilder<WorkflowStep> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StepName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(s => s.AssignedTo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.ActionType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.NextStep)
            .HasMaxLength(150);
    }
}
