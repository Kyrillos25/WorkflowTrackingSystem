using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcessById;
public sealed record GetProcessByIdCommand(Guid Id) : ICommand<GetProcessModel>;
