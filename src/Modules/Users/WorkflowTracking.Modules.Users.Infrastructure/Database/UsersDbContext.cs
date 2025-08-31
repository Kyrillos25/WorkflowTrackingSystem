using WorkflowTracking.Common.Infrastructure.Inbox;
using WorkflowTracking.Common.Infrastructure.Outbox;
using WorkflowTracking.Modules.Users.Application.Abstractions.Data;
using WorkflowTracking.Modules.Users.Domain.Invitations;
using WorkflowTracking.Modules.Users.Domain.Users;
using WorkflowTracking.Modules.Users.Infrastructure.Invitations;
using WorkflowTracking.Modules.Users.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace WorkflowTracking.Modules.Users.Infrastructure.Database;
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Invitation> Invitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
    }
}
