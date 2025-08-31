using WorkflowTracking.Common.Application.EventBus;

namespace WorkflowTracking.Modules.Users.IntegrationEvents;
public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string email,
        string firstName,
        string lastName,
        string mobile)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
    }

    public Guid UserId { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Mobile { get; init; }
}
