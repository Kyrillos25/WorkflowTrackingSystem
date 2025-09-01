using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Common.Presentation.Results;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
using WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcess;

namespace WorkflowTracking.Modules.WFProcessor.Presentation.WFProcessor;
internal sealed class GetProcess : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("WFProcessor/v1/processes", async ([AsParameters] GetProcessesQuery request, ISender sender) =>
        {
            Result<List<GetProcessQueryResponse>> processQueryResult = await sender.Send(new GetProcessCommand(request.WorkflowId, request.Status, request.AssignedTo));
            return processQueryResult.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFProcessors);
    }
   
}
public record GetProcessesQuery(
   [property: FromQuery(Name = "workflow_id")] Guid? WorkflowId,
   [property: FromQuery(Name = "status")] string? Status,
   [property: FromQuery(Name = "assigned_to")] string? AssignedTo
   );
