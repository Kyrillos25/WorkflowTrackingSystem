namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.CreateWorkflow;
public sealed record WorkflowModel(string Name, string Descririptionb, List<WorkflowStepModel> Steps);
