using WorkflowTracking.Common.Application.Messaging;
using WorkflowTracking.Common.Domain;
using WorkflowTracking.Modules.Users.Application.Abstractions.Data;
using WorkflowTracking.Modules.Users.Domain.Users;

namespace WorkflowTracking.Modules.Users.Application.Users.UpdateUser;
internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
