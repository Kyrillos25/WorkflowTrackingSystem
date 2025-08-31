using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.CreateWorkflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.CreateWorkflow;
public sealed record CreateWorkflowCommand(string Name, string Description, List<WorkflowStepModel> steps) : ICommand<Guid>;
