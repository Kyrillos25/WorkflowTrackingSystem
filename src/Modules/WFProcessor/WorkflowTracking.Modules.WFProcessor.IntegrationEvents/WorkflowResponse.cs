using System.Text.Json.Serialization;

namespace WorkflowTracking.Modules.WFProcessor.IntegrationEvents;
public class WorkflowResponse
{
    public List<GetWorkflowModel> results { get; set; } = new();
}


public sealed record GetWorkflowModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("steps")] List<GetWorkflowStepModel> Steps);

public sealed record GetWorkflowStepModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("step_name")] string StepName,
    [property: JsonPropertyName("assigned_to")] string AssignedTo,
    [property: JsonPropertyName("action_type")] string ActionType,
    [property: JsonPropertyName("next_step")] string NextStep);
