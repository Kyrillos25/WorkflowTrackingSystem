using System.Linq;
using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.Abstractions.Model.GetWorkflow;
using WorkflowTracking.Modules.WFManagment.Domain.Workflow;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflowById;
internal sealed class GetWorkflowByIdCommandHandler(
    IWorkflowRepository workflowRepository)
    : ICommandHandler<GetWorkflowByIdCommand, GetWorkflowModel>
{
    public async Task<Result<GetWorkflowModel>> Handle(GetWorkflowByIdCommand request, CancellationToken cancellationToken)
    {
        Workflow? workflow = await workflowRepository.GetByIdAsync(request.Id, cancellationToken);
        if (workflow is null)
        {
            return Result.Failure<GetWorkflowModel>(Error.NotFound(
                code: "Workflow.NotFound",
                description: $"Workflow '{request.Id}' was not found."));
        }

        var model = new GetWorkflowModel(
            workflow.Id.ToString(),
            workflow.Name,
            workflow.Description,
            workflow.Steps.Select(s => new GetWorkflowStepModel(
                s.Id.ToString(),
                s.StepName,
                s.AssignedTo,
                s.ActionType,
                s.NextStep)).ToList());

        return Result.Success(model);
    }
    
}
