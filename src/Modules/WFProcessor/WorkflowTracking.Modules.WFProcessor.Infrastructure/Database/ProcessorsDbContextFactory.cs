using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Database;
public sealed class ProcessorsDbContextFactory : IDesignTimeDbContextFactory<ProcessorsDbContext>
{
    public ProcessorsDbContext CreateDbContext(string[] args)
    {
        // Build configuration to resolve the Database connection string at design-time.
        // Searches multiple base paths so you can run EF from the solution root.
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        // Candidate base paths to probe for appsettings.json
        string cwd = Directory.GetCurrentDirectory();
        string apiProjectDir = Path.Combine(cwd, "src", "API", "WorkflowTracking.WFManagement.API");

        IConfigurationRoot? configuration = null;
        foreach (string basePath in new[] { cwd, apiProjectDir })
        {
            if (File.Exists(Path.Combine(basePath, "appsettings.json")))
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
                break;
            }
        }

        configuration ??= new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        string connectionString = configuration.GetConnectionString("Database");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'Database' was not found. Provide it via appsettings (API project) or the environment variable ConnectionStrings__Database.");
        }

        DbContextOptionsBuilder<ProcessorsDbContext> optionsBuilder = new DbContextOptionsBuilder<ProcessorsDbContext>()
            .UseNpgsql(
                connectionString,
                npgsql => npgsql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Processors))
            .UseSnakeCaseNamingConvention();

        return new ProcessorsDbContext(optionsBuilder.Options);
    }
}

