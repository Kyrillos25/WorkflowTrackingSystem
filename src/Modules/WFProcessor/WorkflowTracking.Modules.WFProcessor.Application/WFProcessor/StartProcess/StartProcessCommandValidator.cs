using FluentValidation;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.StartProcess;
internal sealed class StartProcessCommandValidator : AbstractValidator<StartProcessCommand>
{
    public StartProcessCommandValidator()
    {
        RuleFor(c => c.WorkflowId).NotEmpty();
        RuleFor(c => c.Initiator).NotEmpty();
    }
}
