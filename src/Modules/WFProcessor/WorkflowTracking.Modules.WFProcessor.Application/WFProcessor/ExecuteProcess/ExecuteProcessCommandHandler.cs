using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Application.Models.GetWorkflow;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.ExecuteProcess;
internal sealed class ExecuteProcessCommandHandler(
    IProcessStepExecutionRepository processStepExecutionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ExecuteProcessCommand>
{
    public async Task<Result> Handle(ExecuteProcessCommand request, CancellationToken cancellationToken)
    {
        List<ProcessStepExecution> processStepExecutions = await processStepExecutionRepository.GetCompletedSteps(request.ProcessId, cancellationToken);
        Error validationError = ValidateCurrentStep(request.ProcessId, request.StepName, request.PerformedBy, request.workflow.Steps, processStepExecutions);
        var processStepExecution = ProcessStepExecution.Create(request.StepName, request.PerformedBy, request.Action);
        processStepExecution.ProcessId = request.ProcessId;
        if (validationError is not null)
        {
            processStepExecution.Status = "Failed";
            processStepExecutionRepository.Insert(processStepExecution);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Failure<GetProcessModel>(validationError);
        }
        processStepExecutionRepository.Insert(processStepExecution);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    private Error? ValidateCurrentStep(Guid processId, string stepName, string performedBy, List<GetWorkflowStepModel> steps, List<ProcessStepExecution> processStepExecutions)
    {
        GetWorkflowStepModel expectedStep = GetExpectedStep(steps, processStepExecutions);
        if (expectedStep is null)
        {
            return ProcessStepExecutionErrors.NotFound(processId);
        }
        if (!string.Equals(stepName.Replace(" ", ""), expectedStep.StepName.Replace(" ", ""),
                                                  StringComparison.OrdinalIgnoreCase))
        {
            return ProcessStepExecutionErrors.InvalidStepName(stepName);
        }
        if (!string.Equals(performedBy.Replace(" ", ""), expectedStep.AssignedTo.Replace(" ", ""),
                                                  StringComparison.OrdinalIgnoreCase))
        {
            return ProcessStepExecutionErrors.InvalidRole(performedBy);
        }
        return null;
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
