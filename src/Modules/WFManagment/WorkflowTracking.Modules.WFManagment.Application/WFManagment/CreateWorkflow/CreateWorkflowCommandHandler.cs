using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.CreateWorkflow;
internal sealed class CreateWorkflowCommandHandler(
    IWorkflowRepository workflowRepository,
    IWorkflowStepRepository workflowStepRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateWorkflowCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = Workflow.Create(request.Name, request.Description);
        workflowRepository.Insert(workflow);
        await workflowStepRepository.Insert(workflow.Id, request.steps?.Select(s => WorkflowStep.Create(s.StepName, s.AssignedTo, s.ActionType, s.NextStep)).ToList() ?? new List<WorkflowStep>(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return workflow.Id;
    }
}
