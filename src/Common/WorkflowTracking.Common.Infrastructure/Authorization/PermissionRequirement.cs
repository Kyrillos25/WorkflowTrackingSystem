using Microsoft.AspNetCore.Authorization;

namespace WorkflowTracking.Common.Infrastructure.Authorization;
internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}