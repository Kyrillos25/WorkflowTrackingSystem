using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflowById;
public sealed record GetWorkflowByIdCommand(Guid Id) : ICommand<GetWorkflowModel>;
