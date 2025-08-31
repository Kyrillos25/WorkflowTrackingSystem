using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;
using System.Linq;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.UpdateWorkflow;
internal sealed class UpdateWorkflowCommandHandler(
    IWorkflowRepository workflowRepository,
    IWorkflowStepRepository workflowStepRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateWorkflowCommand>
{
    public async Task<Result> Handle(UpdateWorkflowCommand request, CancellationToken cancellationToken)
    {
        Workflow? workflow = await workflowRepository.GetByIdAsync(request.Id, cancellationToken);

        if (workflow is null)
        {
            return Result.Failure(WorkflowErrors.NotFound(request.Id));
        }
        workflow.Update(request.Name, request.Description);
        await workflowStepRepository.UpdateAsync(workflow.Id, request.steps?.Select(s => WorkflowStep.Create(s.StepName, s.AssignedTo, s.ActionType, s.NextStep)).ToList() ?? new List<WorkflowStep>(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
