using WorkflowTracking.Common.Application.Messaging;

namespace WorkflowTracking.Modules.Users.Application.Users.RegisterUser;
public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName, string Mobile)
    : ICommand<Guid>;

