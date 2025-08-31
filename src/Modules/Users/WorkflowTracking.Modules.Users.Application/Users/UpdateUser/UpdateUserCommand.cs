using WorkflowTracking.Common.Application.Messaging;

namespace WorkflowTracking.Modules.Users.Application.Users.UpdateUser;
public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
