using FluentValidation;

namespace WorkflowTracking.Modules.WFProcessor.Application.WFProcessor.GetProcessById;
internal sealed class GetProcessByIdCommandValidator : AbstractValidator<GetProcessByIdCommand>
{
    public GetProcessByIdCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
