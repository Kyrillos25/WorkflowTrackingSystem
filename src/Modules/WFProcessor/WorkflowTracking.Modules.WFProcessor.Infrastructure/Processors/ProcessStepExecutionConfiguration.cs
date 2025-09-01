using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Processors;
internal sealed class ProcessStepExecutionConfiguration : IEntityTypeConfiguration<ProcessStepExecution>
{
    public void Configure(EntityTypeBuilder<ProcessStepExecution> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StepName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(s => s.PerformedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Action)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.Status)
            .HasMaxLength(10);
    }
}
