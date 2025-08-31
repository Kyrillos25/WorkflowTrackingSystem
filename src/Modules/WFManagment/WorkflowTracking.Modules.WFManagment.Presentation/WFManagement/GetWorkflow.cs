using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Common.Presentation.Results;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;
using WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflow;
using WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflowById;

namespace WorkflowTracking.Modules.WFManagment.Presentation.WFManagement;
internal sealed class GetWorkflow : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("WFManagement/v1/Workflows", async (ISender sender) =>
        {
            Result<List<GetWorkflowModel>> result = await sender.Send(new GetWorkflowCommand());
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFManagements);
    }
}
