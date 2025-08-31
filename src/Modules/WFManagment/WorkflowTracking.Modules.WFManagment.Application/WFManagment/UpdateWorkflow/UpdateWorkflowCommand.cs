using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.CreateWorkflow;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.UpdateWorkflow;
public sealed record UpdateWorkflowCommand(Guid Id, string Name, string Description, List<WorkflowStepModel> steps) : ICommand;
