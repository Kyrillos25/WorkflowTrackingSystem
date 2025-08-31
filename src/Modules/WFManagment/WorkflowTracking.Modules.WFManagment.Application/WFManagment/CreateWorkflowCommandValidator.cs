using FluentValidation;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment;
internal sealed class CreateWorkflowCommandValidator : AbstractValidator<CreateWorkflowCommand>
{
    public CreateWorkflowCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}
