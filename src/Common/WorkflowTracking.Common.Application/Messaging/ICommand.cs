using MediatR;
using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Common.Application.Messaging;
public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
