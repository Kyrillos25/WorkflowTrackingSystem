using FluentValidation;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.CreateWorkflow;
internal sealed class CreateWorkflowCommandValidator : AbstractValidator<CreateWorkflowCommand>
{
    public CreateWorkflowCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();

        RuleFor(c => c.steps)
            .NotNull()
            .NotEmpty().WithMessage("Steps must not be empty.");

        RuleForEach(c => c.steps).ChildRules(step =>
        {
            step.RuleFor(s => s.StepName).NotEmpty();
            step.RuleFor(s => s.AssignedTo).NotEmpty();
            step.RuleFor(s => s.ActionType).NotEmpty();
            step.RuleFor(s => s.NextStep).NotEmpty();
        });
    }
}
