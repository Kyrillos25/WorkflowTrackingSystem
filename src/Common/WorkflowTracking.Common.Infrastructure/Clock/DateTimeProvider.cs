using WorkflowTracking.Common.Application.Clock;

namespace WorkflowTracking.Common.Infrastructure.Clock;
internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
