using System.Text.Json.Serialization;

namespace WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
public sealed record GetProcessQueryResponse(
    [property: JsonPropertyName("workflow_id")] string WorkflowId,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("assigned_to")] string AssignedTo);
