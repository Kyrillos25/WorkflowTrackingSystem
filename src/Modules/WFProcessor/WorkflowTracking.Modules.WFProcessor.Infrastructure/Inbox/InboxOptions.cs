namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Inbox;
internal sealed class InboxOptions
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}

