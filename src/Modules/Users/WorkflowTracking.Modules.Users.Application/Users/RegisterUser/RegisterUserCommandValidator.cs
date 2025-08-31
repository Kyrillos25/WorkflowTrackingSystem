using FluentValidation;

namespace WorkflowTracking.Modules.Users.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).MinimumLength(6);
        RuleFor(c => c.Mobile).NotEmpty().Matches(@"^01[0125][0-9]{8}$");
    }
}
