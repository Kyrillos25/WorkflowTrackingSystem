using MediatR;
using System.Linq;
using System.Text.Json.Serialization;
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
            List<Application.Abstractions.Service.WorkflowStepModel> steps = request.Steps?.Select(s => new Application.Abstractions.Service.WorkflowStepModel(
                s.StepName,
                s.AssignedTo,
                s.ActionType,
                s.NextStep)).ToList() ?? new List<Application.Abstractions.Service.WorkflowStepModel>();

            Result<Guid> result = await sender.Send(new CreateWorkflowCommand(
                request.Name,
                request.Description,
                steps));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFManagements);
    }

    internal sealed class Request
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("steps")]
        public List<WorkflowStepRequest> Steps { get; init; }
    }
    internal sealed class WorkflowStepRequest
    {
        [JsonPropertyName("step_name")]
        public string StepName { get; init; }
        [JsonPropertyName("assigned_to")]
        public string AssignedTo { get; init; }
        [JsonPropertyName("action_type")]
        public string ActionType { get; init; }
        [JsonPropertyName("next_step")]
        public string NextStep { get; init; }
    }
}
