using FluentValidation;

namespace WorkflowTracking.Modules.WFManagment.Application.WFManagment.GetWorkflowById;
internal sealed class GetWorkflowByIdCommandValidator : AbstractValidator<GetWorkflowByIdCommand>
{
    public GetWorkflowByIdCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
