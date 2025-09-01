using WorkflowTracking.Common.Application.Messaging;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.StartProcess;
public sealed record StartProcessCommand(Guid WorkflowId, string Initiator) : ICommand<Guid>;
