using FluentValidation;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.ExecuteProcess;
internal sealed class ExecuteProcessCommandValidator : AbstractValidator<ExecuteProcessCommand>
{
    public ExecuteProcessCommandValidator()
    {
        RuleFor(c => c.ProcessId).NotEmpty();
        RuleFor(c => c.StepName).NotEmpty();
        RuleFor(c => c.PerformedBy).NotEmpty();
        RuleFor(c => c.Action).NotEmpty();
    }
}
