﻿using Microsoft.OpenApi.Models;

namespace WorkflowTracking.WorkflowManagement.API.Extensions;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Workflow Management API",
                Version = "v1",
                Description = "API built using the modular monolith architecture."
            });

            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });

        return services;
    }
}
