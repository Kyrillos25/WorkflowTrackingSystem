using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Application.Models.GetWorkflow;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcess;
internal sealed class GetProcessCommandHandler(
    IProcessRepository processRepository,
    IProcessStepExecutionRepository processStepExecutionRepository)
    : ICommandHandler<GetProcessCommand, List<GetProcessQueryModel>>
{
    public async Task<Result<List<GetProcessQueryModel>>> Handle(GetProcessCommand request, CancellationToken cancellationToken)
    {
        var result = new List<GetProcessQueryModel>();
        foreach (GetWorkflowModel workflowModel in request.WorkflowModels)
        {
            Process process = await processRepository.GetByWorkFlowIdAsync(new Guid(workflowModel.Id), cancellationToken);
            if (process is null)
            {
                result.Add(new GetProcessQueryModel(workflowModel.Id, "Active", workflowModel.Steps?[0].AssignedTo ?? string.Empty));
                continue;
            }
            List<ProcessStepExecution> completedSteps = await processStepExecutionRepository.GetCompletedSteps(process.Id, cancellationToken);
            string status = (completedSteps.Count == workflowModel.Steps?.Count) ? "Completed" : "Pending";
            GetWorkflowStepModel? step = GetExpectedStep(workflowModel.Steps?? new List<GetWorkflowStepModel>(), completedSteps);
            result.Add(new GetProcessQueryModel(workflowModel.Id, status, step?.AssignedTo ?? workflowModel.Steps?[0].AssignedTo ?? string.Empty));
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
