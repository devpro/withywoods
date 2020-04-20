using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Withywoods.AspNetCore.Builder;
using Withywoods.AspNetCore.DependencyInjection;

namespace Withywoods.AspNetCoreApiSample
{
    /// <summary>
    /// Web application startup class.
    /// </summary>
    public class Startup
    {
        private readonly WebAppConfiguration _webAppConfiguration;

        /// <summary>
        /// Create a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _webAppConfiguration = new WebAppConfiguration(configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Health checks
            services.AddHealthChecks()
                .AddDbContextCheck<Dal.Efcore.EfcoreDbContext>();

            // Controllers
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(_webAppConfiguration);

            // DAL
            services.AddScoped<Dal.ITaskDbContext, Dal.Efcore.EfcoreDbContext>();
            services.AddScoped<Dal.Repositories.ITaskRepository, Dal.Efcore.Repositories.TaskRepository>();
            services.AddDbContext<Dal.Efcore.EfcoreDbContext>(opt =>
                opt.UseInMemoryDatabase(_webAppConfiguration.InMemoryDatabaseName));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger(_webAppConfiguration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(_webAppConfiguration.HealthChecksEndpoint);
                endpoints.MapControllers();
            });
        }
    }
}
