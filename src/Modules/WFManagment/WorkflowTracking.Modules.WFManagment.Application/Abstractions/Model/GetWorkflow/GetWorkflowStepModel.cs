using System.Text.Json.Serialization;

namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;
public sealed record GetWorkflowStepModel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("step_name")] string StepName,
    [property: JsonPropertyName("assigned_to")] string AssignedTo,
    [property: JsonPropertyName("action_type")] string ActionType,
    [property: JsonPropertyName("next_step")] string NextStep);
