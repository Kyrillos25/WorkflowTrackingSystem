namespace WorkflowTracking.Modules.Users.Domain.Invitations;
public interface IInvitationRepository
{
    Task<Invitation?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Invitation invitation);
}
