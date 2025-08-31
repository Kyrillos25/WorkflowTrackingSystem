using MediatR;
using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Common.Application.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
