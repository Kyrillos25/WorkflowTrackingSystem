using Microsoft.AspNetCore.Routing;

namespace WorkflowTracking.Common.Presentation.Endpoints;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
