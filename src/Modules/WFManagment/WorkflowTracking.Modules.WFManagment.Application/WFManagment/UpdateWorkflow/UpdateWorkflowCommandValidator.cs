using FluentValidation;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.UpdateWorkflow;
internal sealed class UpdateWorkflowCommandValidator : AbstractValidator<UpdateWorkflowCommand>
{
    public UpdateWorkflowCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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
