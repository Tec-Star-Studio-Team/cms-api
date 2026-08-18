using CmsApi.Application.Common.Behaviors;
using CmsApi.Infrastructure;
using CmsApi.Presentation.Middleware;
using CmsApi.Server.Infrastructure.Persistence;
using CmsApi.Server.Presentation;
using FluentValidation;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

// Validation pipeline behavior
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

// FluentValidation — scans all validators in the assembly
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    // Adds the 'Authorization: Bearer <token>' button in Scalar UI
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token."
        });

        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    // Apply pending migrations automatically on startup
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync(); // ← applies pending migrations
    }

    app.MapOpenApi();

    // Scalar UI available at /scalar/v1
    app.MapScalarApiReference(options =>
    {
        options.Title = "CMS API";
        options.Theme = ScalarTheme.DeepSpace;

        // Enables the JWT Bearer input field in the UI
        options.AddHttpAuthentication("Bearer", auth =>
        {
            auth.Description = "Enter your JWT token.";
        });
    });

    app.UseCors(CorsPolicy.Development); // ← development policy
}
else
{
    app.UseCors(CorsPolicy.Production); // ← production policy
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

var api = app.MapGroup("/api");
api.MapGet("weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultEndpoints();
app.MapEndpoints();

app.UseFileServer();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
