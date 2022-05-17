var builder = WebApplication.CreateBuilder(args);

var configuration = new WebAppConfiguration(builder.Configuration);

// adds services to the container
builder.Services.AddHealthChecks().AddDbContextCheck<Withywoods.AspNetCoreApiSample.Dal.Efcore.EfcoreDbContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(configuration.OpenApi);
builder.Services.AddScoped<Withywoods.AspNetCoreApiSample.Dal.ITaskDbContext, Withywoods.AspNetCoreApiSample.Dal.Efcore.EfcoreDbContext>();
builder.Services.AddScoped<Withywoods.AspNetCoreApiSample.Dal.Repositories.ITaskRepository, Withywoods.AspNetCoreApiSample.Dal.Efcore.Repositories.TaskRepository>();
builder.Services.AddDbContext<Withywoods.AspNetCoreApiSample.Dal.Efcore.EfcoreDbContext>(opt => opt.UseInMemoryDatabase(global::Withywoods.AspNetCoreApiSample.WebAppConfiguration.InMemoryDatabaseName));

var app = builder.Build();

// configures the HTTP request pipeline
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseHsts();
app.UseSwagger(configuration.OpenApi);
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks(global::Withywoods.AspNetCoreApiSample.WebAppConfiguration.HealthChecksEndpoint);
    endpoints.MapControllers();
});

app.Run();

// fix: make Program class public for tests
#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050
