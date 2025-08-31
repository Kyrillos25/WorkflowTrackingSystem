using System.Reflection;
using WorkflowTracking.Common.Application;
using WorkflowTracking.Common.Infrastructure;
using WorkflowTracking.Common.Infrastructure.Configuration;
using WorkflowTracking.Common.Infrastructure.EventBus;
using WorkflowTracking.Common.Presentation.Endpoints;
using WorkflowTracking.Modules.Users.Infrastructure;
using WorkflowTracking.Users.API.Extensions;
using WorkflowTracking.Users.API.Middleware;
using WorkflowTracking.Users.API.OpenTelemetry;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

Assembly[] moduleApplicationAssemblies = [WorkflowTracking.Modules.Users.Application.AssemblyReference.Assembly];

builder.Services.AddApplication(moduleApplicationAssemblies);

string databaseConnectionString = builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnectionString = builder.Configuration.GetConnectionStringOrThrow("Cache");
var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow("Queue"));

builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [
        //TicketingModule.ConfigureConsumers
    ],
    rabbitMqSettings,
    databaseConnectionString,
    redisConnectionString);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddRabbitMQ(sp =>
    {
        return new ConnectionFactory
        {
            HostName = rabbitMqSettings.Host,
            UserName = rabbitMqSettings.Username,
            Password = rabbitMqSettings.Password
        }.CreateConnectionAsync();
    })
    .AddKeyCloak(keyCloakHealthUrl);



builder.Services.AddUsersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContextTraceLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();

internal partial class Program;
