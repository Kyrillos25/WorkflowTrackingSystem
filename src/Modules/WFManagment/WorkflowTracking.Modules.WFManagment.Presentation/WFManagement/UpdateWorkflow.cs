using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Common.Presentation.Results;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.CreateWorkflow;
using WorkflowTracking.Modules.WFManagment.Application.WFManagment.UpdateWorkflow;

namespace WorkflowTracking.Modules.WFManagment.Presentation.WFManagement;
internal sealed class UpdateWorkflow : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("WFManagement/v1/Workflows", async (Request request, ISender sender) =>
        {
            List<WorkflowStepModel> steps = request.Steps?.Select(s => new Application.Abstractions.Model.CreateWorkflow.WorkflowStepModel(
                s.StepName,
                s.AssignedTo,
                s.ActionType,
                s.NextStep)).ToList() ?? new List<WorkflowStepModel>();

            Result result = await sender.Send(new UpdateWorkflowCommand(
                request.Id,
                request.Name,
                request.Description,
                steps));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFManagements);
    }

    internal sealed class Request
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
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
