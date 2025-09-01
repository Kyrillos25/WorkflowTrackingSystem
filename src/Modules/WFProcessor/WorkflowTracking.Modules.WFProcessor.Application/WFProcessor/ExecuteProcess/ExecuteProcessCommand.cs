using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.ExecuteProcess;
public sealed record ExecuteProcessCommand(Guid ProcessId, string StepName, string PerformedBy, string Action, GetProcessModel Process) : ICommand;
