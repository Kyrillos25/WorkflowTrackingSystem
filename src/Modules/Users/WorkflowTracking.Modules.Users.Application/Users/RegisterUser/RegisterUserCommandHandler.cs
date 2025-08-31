using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.Users.Application.Abstractions.Data;
using WorkflowTracking.Modules.Users.Application.Abstractions.Identity;
using WorkflowTracking.Modules.Users.Domain.Users;

namespace WorkflowTracking.Modules.Users.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = await identityProviderService.RegisterUserAsync(
            new UserModel(request.Email, request.Password, request.FirstName, request.LastName, request.Mobile),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var user = User.Create(request.Email, request.FirstName, request.LastName, result.Value, request.Mobile);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
