using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Withywoods.AspNetCore.Builder;
using Withywoods.AspNetCore.DependencyInjection;

namespace Withywoods.AspNetCoreApiSample
{
    /// <summary>
    /// Web application startup class.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ApiConfiguration _apiConfiguration;

        /// <summary>
        /// Create a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiConfiguration = new ApiConfiguration(_configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Swagger
            services.AddSwaggerGen(_apiConfiguration);

            // DAL
            services.AddScoped<Dal.ITodoDbContext, Dal.Efcore.EfcoreDbContext>();
            services.AddDbContext<Dal.Efcore.EfcoreDbContext>(opt =>
                opt.UseInMemoryDatabase(_apiConfiguration.InMemoryDatabaseName));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseSwagger(_apiConfiguration);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
