using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Workflows;
internal sealed class WorfkflowConfiguration : IEntityTypeConfiguration<Workflow>
{
    public void Configure(EntityTypeBuilder<Workflow> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).HasMaxLength(200);

        builder.Property(u => u.Description).HasMaxLength(500);

        builder.HasMany(w => w.Steps)
               .WithOne(s => s.Workflow)
               .HasForeignKey(s => s.WorkflowId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
