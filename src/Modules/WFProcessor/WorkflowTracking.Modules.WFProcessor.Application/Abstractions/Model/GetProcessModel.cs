using System.Text.Json.Serialization;

namespace WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
public sealed record GetProcessModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("workflow_id")] string WorkflowId,
    [property: JsonPropertyName("initiator")] string Initiator);

