using MassTransit;
using MediatR;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflow;
using WorkflowTracking.Modules.WFProcessor.IntegrationEvents;

namespace WorkflowTracking.Modules.WFManagment.Presentation.Handlers;

public sealed class GetWorkflowConsumer(ISender sender) : IConsumer<GetWorkflowIntegrationEvent>
{
    public async Task Consume(ConsumeContext<GetWorkflowIntegrationEvent> context)
    {
        Result<List<Application.Abstractions.Model.GetWorkflow.GetWorkflowModel>> result = await sender.Send(new GetWorkflowCommand());

        var response = new WorkflowResponse
        {
            results = result.Value.Select(x => new WFProcessor.IntegrationEvents.GetWorkflowModel(x.Id, x.Name, x.Description, 
            x.Steps.Select(s => new WFProcessor.IntegrationEvents.GetWorkflowStepModel(s.Id, s.StepName, s.AssignedTo, s.ActionType, s.NextStep)).ToList())).ToList()
        };

        await context.RespondAsync(response);
    }
}


