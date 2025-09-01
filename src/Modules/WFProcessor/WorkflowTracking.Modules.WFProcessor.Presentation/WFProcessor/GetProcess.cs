using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Application.Commands.WFManagement;
using WorkflowTracking.Common.Application.Models.GetWorkflow;
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
            if (request.WorkflowId is not null)
            {
                Result<GetWorkflowModel> workflowResult = await sender.Send(new GetWorkflowByIdCommand(request.WorkflowId.Value));
                if (workflowResult.Value is null)
                {
                    return workflowResult.Match(Results.NotFound, ApiResults.Problem);
                }
                Result<List<GetProcessQueryModel>> processQueryResult = await sender.Send(new GetProcessCommand(request.Status, request.AssignedTo, new List<GetWorkflowModel>() { workflowResult.Value }));
                return processQueryResult.Match(Results.Ok, ApiResults.Problem);
            }
            else
            {
                Result<List<GetWorkflowModel>> workflowResults = await sender.Send(new GetWorkflowCommand());
                Result<List<GetProcessQueryModel>> processQueryResult = await sender.Send(new GetProcessCommand(request.Status, request.AssignedTo, workflowResults.Value));
                return processQueryResult.Match(Results.Ok, ApiResults.Problem);
            }
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
