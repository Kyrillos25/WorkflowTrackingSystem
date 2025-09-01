using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Application.Models.GetWorkflow;

namespace WorkflowTracking.Common.Application.Commands.WFManagement;
public sealed record GetWorkflowByIdCommand(Guid Id) : ICommand<GetWorkflowModel>;
