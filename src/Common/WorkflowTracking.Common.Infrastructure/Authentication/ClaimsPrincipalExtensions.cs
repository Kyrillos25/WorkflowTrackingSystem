using System.Security.Claims;
using WorkflowTracking.Common.Application.Exceptions;

namespace WorkflowTracking.Common.Infrastructure.Authentication;
public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;

        return Guid.TryParse(userId, out Guid parsedUserId) ?
        parsedUserId :
            throw new WorkflowTrackingException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
               throw new WorkflowTrackingException("User identity is unavailable");
    }

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                              throw new WorkflowTrackingException("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}

