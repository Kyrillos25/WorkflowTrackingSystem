using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Common.Application.Authorization;
public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}

