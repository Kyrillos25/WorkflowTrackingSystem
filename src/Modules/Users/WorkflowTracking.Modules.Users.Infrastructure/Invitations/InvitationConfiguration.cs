using WorkflowTracking.Modules.Users.Domain.Invitations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkflowTracking.Modules.Users.Infrastructure.Invitations;
internal sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Mobile).IsRequired().HasMaxLength(11);

        builder.Property(u => u.ClassroomId).HasMaxLength(200);

        builder.Property(i => i.Status)
           .HasConversion<string>() // store enum as string (optional, remove if you want int)
           .IsRequired();

        builder.Property(i => i.ExpiresAt)
            .IsRequired();
    }
}
