using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Common.Presentation.Results;
using WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.StartProcess;

namespace WorkflowTracking.Modules.WFProcessor.Presentation.WFProcessor;
internal sealed class StartProcess : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("WFProcessor/v1/processes/start", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new StartProcessCommand(
                request.WorkflowId,
                request.Initiator));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.WFProcessors);
    }

    internal sealed class Request
    {
        [JsonPropertyName("workflowId")]
        public Guid WorkflowId { get; init; }

        [JsonPropertyName("initiator")]
        public string Initiator { get; init; }
    }
}
