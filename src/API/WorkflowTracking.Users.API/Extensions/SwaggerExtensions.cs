using Microsoft.OpenApi.Models;

namespace WorkflowTracking.Users.API.Extensions;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Workflow Users API",
                Version = "v1",
                Description = "Workflow Users API built using the modular monolith architecture."
            });

            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });

        return services;
    }
}
