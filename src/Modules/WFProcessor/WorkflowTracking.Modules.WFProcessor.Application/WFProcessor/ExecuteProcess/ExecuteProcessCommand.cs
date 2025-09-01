using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Application.Models.GetWorkflow;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.ExecuteProcess;
public sealed record ExecuteProcessCommand(Guid ProcessId, string StepName, string PerformedBy, string Action, GetProcessModel Process, GetWorkflowModel workflow) : ICommand;
