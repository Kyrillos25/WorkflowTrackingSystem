using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.WFProcessor.Application.Abstractions.Data;
using WorkflowTracking.Modules.WFProcessor.Domain.Processor;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.StartProcess;
internal sealed class StartProcessCommandHandler(
    IProcessRepository processRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<StartProcessCommand, Guid>
{
    public async Task<Result<Guid>> Handle(StartProcessCommand request, CancellationToken cancellationToken)
    {
        var process = Process.Create(request.WorkflowId, request.Initiator);
        processRepository.Insert(process);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return process.Id;
    }
}
