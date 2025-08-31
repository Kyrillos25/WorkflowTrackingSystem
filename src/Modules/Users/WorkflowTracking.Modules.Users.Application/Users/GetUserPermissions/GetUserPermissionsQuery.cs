using WorkflowTracking.Common.Application.Authorization;
using WorkflowTracking.Common.Application.Messaging;

namespace WorkflowTracking.Modules.Users.Application.Users.GetUserPermissions;
public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
