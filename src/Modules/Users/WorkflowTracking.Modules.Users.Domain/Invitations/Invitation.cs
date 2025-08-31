using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.Users.Domain.Invitations;
public sealed class Invitation : Entity
{
    public Guid Id { get; private set; }
    public string Mobile { get; private set; } = null!;
    public Guid? ClassroomId { get; private set; } // optional link
    public InvitationStatus Status { get; private set; } = InvitationStatus.Pending;
    public DateTime ExpiresAt { get; private set; }

    private Invitation() { }
    public Invitation(string mobile, Guid? classroomId, DateTime expiresAt)
    {
        Mobile = mobile;
        ClassroomId = classroomId;
        ExpiresAt = expiresAt;
    }

    public void Accept() => Status = InvitationStatus.Accepted;
    public void Cancel() => Status = InvitationStatus.Cancelled;
}
