using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Common.Presentation.Results;
using WorkflowTracking.Modules.WFManagment.Application.WFManagment;


namespace WorkflowTracking.Modules.WFManagment.Presentation.WFManagement;
internal sealed class CreateWorkflow : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("WFManagement/v1/Workflows", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateWorkflowCommand(
                request.Name,
                request.Description,new List<Application.Abstractions.Service.WorkflowStepModel>() { }));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFManagements);
    }

    internal sealed class Request
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public List<WorkflowStepRequest> Steps { get; init; }
    }
    internal sealed class WorkflowStepRequest
    {
        public string Name { get; init; }
        public string StepName { get; init; }
        public string AssignedTo { get; init; }
        public string ActionType { get; init; }
        public string NextStep { get; init; }
    }
}
