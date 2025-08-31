using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Common.Application.Exceptions;
public sealed class WorkflowTrackingException : Exception
{
    public WorkflowTrackingException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
