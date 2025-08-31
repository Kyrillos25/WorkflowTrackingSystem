using Npgsql;
using System.Data.Common;
using WorkflowTracking.Common.Application.Data;

namespace WorkflowTracking.Common.Infrastructure.Data;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}