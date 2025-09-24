using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using UserRepository;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(
        ctx.Configuration["ApplicationInsights:InstrumentationKey"],
        Serilog.Events.LogEventLevel.Information)
    .ReadFrom.Configuration(ctx.Configuration)
);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserRepository, UserRepository.UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("UserRepository"))
            .AddAspNetCoreInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddConsoleExporter();
    })
    .WithMetrics(metrics =>
    {
        metrics
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("UserRepository"))
            .AddAspNetCoreInstrumentation()
            .AddPrometheusExporter();
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

