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

        workflowRepository.Insert(workflow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return workflow.Id;
    }
}
