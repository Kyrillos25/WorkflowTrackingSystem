using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflow;
internal sealed class GetWorkflowCommandHandler(
    IWorkflowRepository workflowRepository)
    : ICommandHandler<GetWorkflowCommand, List<GetWorkflowModel>>
{
    public async Task<Result<List<GetWorkflowModel>>> Handle(GetWorkflowCommand request, CancellationToken cancellationToken)
    {
        List<Workflow> workflows = await workflowRepository.GetAsync(cancellationToken);

        var model = workflows.Select(w => new GetWorkflowModel(w.Id.ToString(), w.Name, w.Description,
            w.Steps.Select(ws => new GetWorkflowStepModel(ws.Id.ToString(), ws.StepName, ws.AssignedTo, ws.ActionType, ws.NextStep)).ToList())).ToList();
        return Result.Success(model);
    }

}
