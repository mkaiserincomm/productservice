using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using productservice.Controllers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

namespace productservice
{
    // Get connection string from secrets
    // https://phoenixnap.com/kb/helm-environment-variables

    // To establish proxy for MSSQL Server, run
    // kubectl port-forward service/mssql-headless 1433:1433 --namespace incomm-poc

    // Scaffold a controller
    // dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    // dotnet add package Microsoft.EntityFrameworkCore.Design
    // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    // dotnet tool install -g dotnet-aspnet-codegenerator
    // dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers    

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<LiveCheck>("live_check", null, new[] { "live" })
                .AddCheck<ReadyCheck>("ready_check", null, new[] { "ready" })
                .AddCheck<StartupCheck>("startup_check", null, new[] { "startup" });
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "productservice", Version = "v1" });
            });
                    
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NorthwindContext")));                                             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMetricServer();
            app.UseRequestMiddleware();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "productservice v1"));
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions() { Predicate = p => p.Tags.Contains("live")});
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions() { Predicate = p => p.Tags.Contains("ready")});
                endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions() { Predicate = p => p.Tags.Contains("startup")});
            });
        }
    }
}
