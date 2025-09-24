using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WeatherForecastService
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDbContext<WeatherForecastDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void Configure(WebApplication app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

