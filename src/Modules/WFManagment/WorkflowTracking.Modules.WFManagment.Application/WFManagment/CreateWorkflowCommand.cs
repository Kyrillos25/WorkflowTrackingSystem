using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Service;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment;
public sealed record CreateWorkflowCommand(string Name, string Description, List<WorkflowStepModel> steps) : ICommand<Guid>;
