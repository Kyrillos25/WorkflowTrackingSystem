using MassTransit;
using MediatR;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflowById;
using WorkflowTracking.Modules.WFProcessor.IntegrationEvents;

namespace WorkflowTracking.Modules.WFManagment.Presentation.Handlers;
public sealed class GetWorkflowByIdConsumer(ISender sender) : IConsumer<GetWorkflowByIdIntegrationEvent>
{
    public async Task Consume(ConsumeContext<GetWorkflowByIdIntegrationEvent> context)
    {
        Result<Application.Abstractions.Model.GetWorkflow.GetWorkflowModel> result = await sender.Send(new GetWorkflowByIdCommand(context.Message.WorkflowId));

        var response = new WorkflowResponse
        {
            results = new List<GetWorkflowModel>(){ new WFProcessor.IntegrationEvents.GetWorkflowModel(result.Value.Id, result.Value.Name, result.Value.Description,
            result.Value.Steps.Select(s => new WFProcessor.IntegrationEvents.GetWorkflowStepModel(s.Id, s.StepName, s.AssignedTo, s.ActionType, s.NextStep)).ToList()) }
        };

        await context.RespondAsync(response);
    }
}
