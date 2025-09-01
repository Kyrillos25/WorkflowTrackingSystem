using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.CreateWorkflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.UpdateWorkflow;
public sealed record UpdateWorkflowCommand(Guid Id, string Name, string Description, List<WorkflowStepModel> steps) : ICommand;
