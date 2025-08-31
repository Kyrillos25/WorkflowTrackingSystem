using System.Data.Common;
using WorkflowTracking.Common.Application.Data;
using WorkflowTracking.Common.Application.EventBus;
using WorkflowTracking.Common.Infrastructure.Inbox;
using WorkflowTracking.Common.Infrastructure.Serialization;
using Dapper;
using MassTransit;
using Newtonsoft.Json;

namespace WorkflowTracking.Modules.Users.Infrastructure.Inbox;
internal sealed class IntegrationEventConsumer<TIntegrationEvent>(IDbConnectionFactory dbConnectionFactory)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc
        };

        const string sql =
            """
            INSERT INTO users.inbox_messages(id, type, content, occurred_on_utc)
            VALUES (@Id, @Type, @Content::json, @OccurredOnUtc)
            """;

        await connection.ExecuteAsync(sql, inboxMessage);
    }
}
