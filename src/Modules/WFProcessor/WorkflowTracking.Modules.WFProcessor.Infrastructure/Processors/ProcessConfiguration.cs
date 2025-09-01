using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Processors;
internal sealed class ProcessConfiguration : IEntityTypeConfiguration<Process>
{
    public void Configure(EntityTypeBuilder<Process> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.WorkflowId).IsUnique();

        builder.Property(u => u.Initiator).HasMaxLength(200);

        builder.Property(u => u.Status).HasMaxLength(10);

        builder.HasMany(w => w.Executions)
               .WithOne(s => s.Process)
               .HasForeignKey(s => s.ProcessId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
