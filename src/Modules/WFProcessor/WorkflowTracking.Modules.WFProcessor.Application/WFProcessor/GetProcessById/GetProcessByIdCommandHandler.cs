using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Model;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcessById;
internal sealed class GetProcessByIdCommandHandler(
    IProcessRepository processRepository)
    : ICommandHandler<GetProcessByIdCommand, GetProcessModel>
{
    public async Task<Result<GetProcessModel>> Handle(GetProcessByIdCommand request, CancellationToken cancellationToken)
    {
        Process? process = await processRepository.GetByIdAsync(request.Id, cancellationToken);
        if (process is null)
        {
            return Result.Failure<GetProcessModel>(ProcessErrors.NotFound(request.Id));
        }

        var model = new GetProcessModel(
            process.Id.ToString(),
            process.WorkflowId.ToString(),
            process.Initiator);

        return Result.Success(model);
    }

}
