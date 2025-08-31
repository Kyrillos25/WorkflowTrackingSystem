namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.CreateWorkflow;
public sealed record WorkflowStepModel(string StepName, string AssignedTo, string ActionType, string NextStep);
