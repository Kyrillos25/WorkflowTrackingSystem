using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment;
internal sealed class CreateWorkflowCommandHandler(
    IWorkflowRepository workflowRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateWorkflowCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = Workflow.Create(request.Name, request.Description);

        if (request.steps is not null)
        {
            foreach (Abstractions.Service.WorkflowStepModel step in request.steps)
            {
                workflow.AddStep(step.StepName, step.AssignedTo, step.ActionType, step.NextStep);
            }
        }

        workflowRepository.Insert(workflow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return workflow.Id;
    }
}
