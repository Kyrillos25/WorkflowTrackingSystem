using MediatR;
using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Common.Application.Messaging;
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
