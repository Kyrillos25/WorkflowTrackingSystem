using WorkflowTracking.Common.Application.Messaging;

namespace WorkflowTracking.Modules.Users.Application.Users.GetUser;
public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
