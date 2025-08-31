namespace WorkflowTracking.Modules.WFManagment.Application.Abstractions.Service;
public sealed record WorkflowModel(string Name, string Descririptionb, List<WorkflowStepModel> Steps);
