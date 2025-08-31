namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Service;
public sealed record WorkflowStepModel(string StepName, string AssignedTo, string ActionType, string NextStep);
