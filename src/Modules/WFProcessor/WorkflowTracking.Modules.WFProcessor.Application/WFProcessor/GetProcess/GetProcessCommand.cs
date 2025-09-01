using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcess;
public sealed record GetProcessCommand(Guid? WorkflowId , string? Status, string? AssignedTo) : ICommand<List<GetProcessQueryResponse>>;
