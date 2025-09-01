using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Application.Commands.WFManagement;
using WorkflowTracking.Common.Application.Models.GetWorkflow;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Common.Presentation.Results;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
using WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.ExecuteProcess;
using WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcessById;

namespace WorkflowTracking.Modules.WFProcessor.Presentation.WFProcessor;
internal sealed class ExecuteStep : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("WFProcessor/v1/processes/execute", async (Request request, ISender sender) =>
        {
            Result<GetProcessModel> processResult = await sender.Send(new GetProcessByIdCommand(request.ProcessId));
            if (processResult.Value is null)
            {
                return processResult.Match(Results.NotFound, ApiResults.Problem);
            }
            Result<GetWorkflowModel> workflowResult = await sender.Send(new GetWorkflowByIdCommand(new Guid(processResult.Value.WorkflowId)));
            if (workflowResult.Value is null)
            {
                return workflowResult.Match(Results.NotFound, ApiResults.Problem);
            }

            Result result = await sender.Send(new ExecuteProcessCommand(
                request.ProcessId,
                request.StepName,
                request.PerformedBy,
                request.Action,
                processResult.Value,
                workflowResult.Value));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFProcessors);
    }

    internal sealed class Request
    {
        [JsonPropertyName("process_id")]
        public Guid ProcessId { get; init; }
        [JsonPropertyName("step_name")]
        public string StepName { get; init; }
        [JsonPropertyName("performed_by")]
        public string PerformedBy { get; init; }
        [JsonPropertyName("action")]
        public string Action { get; init; }
    }
}
