using Npgsql;
using Polly;
using Polly.Retry;
using System.Data.Common;
using WorkflowTracking.Common.Application.Data;

namespace WorkflowTracking.Common.Infrastructure.Data;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    private static readonly string[] TransientSqlStates = [
        "57P03", // cannot connect now / in recovery
        "53300", // too many connections
        "08001", // SQL client unable to establish connection
        "08006"  // connection failure
    ];

    private static readonly AsyncRetryPolicy RetryPolicy = Policy
        .Handle<NpgsqlException>(IsTransient)
        .Or<PostgresException>(ex => IsTransient(ex?.SqlState))
        .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await RetryPolicy.ExecuteAsync(async () => await dataSource.OpenConnectionAsync());
    }

    private static bool IsTransient(NpgsqlException ex)
        => ex is not null && IsTransient(ex.SqlState);

    private static bool IsTransient(string? sqlState)
        => !string.IsNullOrEmpty(sqlState) && Array.IndexOf(TransientSqlStates, sqlState) >= 0;
}