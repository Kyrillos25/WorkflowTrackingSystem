using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflow;
public sealed record GetWorkflowCommand : ICommand<List<GetWorkflowModel>>;
