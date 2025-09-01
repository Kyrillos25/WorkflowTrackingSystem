using WorkflowTracking.Common.Application.EventBus;
using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;
using WorkflowTracking.Modules.WFProcessor.IntegrationEvents;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcess;
internal sealed class GetProcessCommandHandler(IEventBus bus,
    IProcessRepository processRepository,
    IProcessStepExecutionRepository processStepExecutionRepository)
    : ICommandHandler<GetProcessCommand, List<GetProcessQueryResponse>>
{
    public async Task<Result<List<GetProcessQueryResponse>>> Handle(GetProcessCommand request, CancellationToken cancellationToken)
    {
        WorkflowResponse workflowResponse = request.WorkflowId is null
            ? await bus.PublishAsync<GetWorkflowIntegrationEvent, WorkflowResponse>(
           new GetWorkflowIntegrationEvent(Guid.NewGuid(), DateTime.Now),
           cancellationToken)
            : await bus.PublishAsync<GetWorkflowByIdIntegrationEvent, WorkflowResponse>(
           new GetWorkflowByIdIntegrationEvent(Guid.NewGuid(), DateTime.Now, request.WorkflowId.Value),
           cancellationToken);

        var result = new List<GetProcessQueryResponse>();
        foreach (GetWorkflowModel workflowModel in workflowResponse.results)
        {
            Process process = await processRepository.GetByWorkFlowIdAsync(new Guid(workflowModel.Id), cancellationToken);
            if (process is null)
            {
                result.Add(new GetProcessQueryResponse(workflowModel.Id, "Active", workflowModel.Steps?[0]?.AssignedTo ?? string.Empty));
                continue;
            }
            List<ProcessStepExecution> completedSteps = await processStepExecutionRepository.GetCompletedSteps(process.Id, cancellationToken);
            string status = (completedSteps.Count == workflowModel.Steps?.Count) ? "Completed" : "Pending";
            GetWorkflowStepModel? step = GetExpectedStep(workflowModel.Steps ?? new List<GetWorkflowStepModel>(), completedSteps);
            result.Add(new GetProcessQueryResponse(workflowModel.Id, status, step?.AssignedTo ?? workflowModel.Steps?[0].AssignedTo ?? string.Empty));
        }

        return Result.Success(result);
    }

    private GetWorkflowStepModel? GetExpectedStep(List<GetWorkflowStepModel> steps, List<ProcessStepExecution> processStepExecutions)
    {
        if (processStepExecutions is null || !processStepExecutions.Any())
        {
            return steps.FirstOrDefault();
        }
        ProcessStepExecution lastExecutedStep = processStepExecutions[0];
        GetWorkflowStepModel? lastStep = steps.Find(x => string.Equals(x.StepName.Replace(" ", ""), lastExecutedStep.StepName.Replace(" ", ""),
                                                  StringComparison.OrdinalIgnoreCase));
        if (lastStep is null)
        {
            return steps.FirstOrDefault();
        }
        return steps.Find(x => string.Equals(x.StepName.Replace(" ", ""), lastStep.NextStep.Replace(" ", ""),
                                                  StringComparison.OrdinalIgnoreCase));
    }
}
