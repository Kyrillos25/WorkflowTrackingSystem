using System.Diagnostics;
using Serilog.Context;

namespace WorkflowTracking.Users.API.Middleware;

internal sealed class LogContextTraceLoggingMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        ActivityTraceId? traceId = Activity.Current?.TraceId;
        string traceIdString = traceId?.ToString() ?? "NoUsersTraceId";
        using (LogContext.PushProperty("TraceId", traceIdString))
        {
            return next.Invoke(context);
        }
    }
}
