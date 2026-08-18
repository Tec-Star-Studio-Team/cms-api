using CmsApi.Application.Common.Behaviors;
using CmsApi.Server.Presentation.Endpoints;
using FluentValidation;
using Mediator;
using System.Reflection;

namespace CmsApi.Server.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Mediator (source generator) — scans all handlers in the assembly
        services.AddMediator(options =>
        {
            options.Namespace = "CmsApi";
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        // Pipeline behavior for validation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // FluentValidation — scans all validators in the assembly
        services.AddValidatorsFromAssemblyContaining<Program>();

        // Scans and registers all IEndpoint implementations in the assembly
        var endpointTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t)
                     && t is { IsInterface: false, IsAbstract: false });

        foreach (var type in endpointTypes)
            services.AddTransient(typeof(IEndpoint), type);

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy.Development, policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000",  // React
                                 "http://localhost:5173")  // Vite
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            options.AddPolicy(CorsPolicy.Production, policy =>
            {
                // Read allowed origins from configuration
                var allowedOrigins = configuration
                    .GetSection("Cors:AllowedOrigins")
                    .Get<string[]>() ?? [];

                policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // Resolves all registered IEndpoint implementations and maps their routes
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoints(app);

        return app;
    }
}
