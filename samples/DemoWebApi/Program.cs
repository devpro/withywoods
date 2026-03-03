var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var configuration = ApplicationConfiguration.Create(builder.Configuration);

var app = builder.Build();

if (configuration.IsScalarEnabled)
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle(configuration.OpenApiInfo.Title ?? "Demo Web Api")
            .WithTheme(ScalarTheme.Kepler)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

if (configuration.IsHttpsRedirectionEnabled)
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.MapHealthChecks(ApplicationConfiguration.HealthCheckEndpoint);

await app.RunAsync();
