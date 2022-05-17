var builder = WebApplication.CreateBuilder(args);

// adds services to the container
builder.Services.AddRazorPages();

var app = builder.Build();

// configures the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();

#pragma warning disable CA1050
public partial class Program { }
#pragma warning restore CA1050
