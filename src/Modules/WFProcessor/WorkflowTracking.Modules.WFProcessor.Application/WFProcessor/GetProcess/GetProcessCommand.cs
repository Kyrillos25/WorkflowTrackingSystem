using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Application.Models.GetWorkflow;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcess;
public sealed record GetProcessCommand(string? Status, string? AssignedTo, List<GetWorkflowModel> WorkflowModels) : ICommand<List<GetProcessQueryModel>>;
