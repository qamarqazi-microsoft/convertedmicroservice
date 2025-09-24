using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using Azure.Monitor.OpenTelemetry.Exporter;

namespace ProductRepository
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ProductRepositoryDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepositoryImpl>();
            services.AddScoped<IProductService, ProductService>();

            services.AddOpenTelemetry()
                .WithTracing(builder => builder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ProductRepository"))
                    .AddAspNetCoreInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddAzureMonitorTraceExporter())
                .WithMetrics(builder => builder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ProductRepository"))
                    .AddAspNetCoreInstrumentation()
                    .AddPrometheusExporter()
                    .AddAzureMonitorMetricExporter());
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapPrometheusScrapingEndpoint();
            });
        }
    }
}

