using System.Text.Json.Serialization;

namespace WorkflowTracking.Common.Application.Models.GetWorkflow;
public sealed record GetWorkflowModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("steps")] List<GetWorkflowStepModel> Steps);
