using WorkflowTracking.Common.Application.Authorization;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace WorkflowTracking.Modules.Users.Infrastructure.Authorization;
internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
