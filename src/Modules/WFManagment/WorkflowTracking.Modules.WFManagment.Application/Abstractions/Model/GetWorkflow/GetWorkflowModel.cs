using System.Text.Json.Serialization;

namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;
public sealed record GetWorkflowModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("steps")] List<GetWorkflowStepModel> Steps);
