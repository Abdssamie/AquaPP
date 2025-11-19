using Microsoft.EntityFrameworkCore;
using Moondesk.DataAccess.Configuration;
using Moondesk.DataAccess.Repositories;
using Serilog;

// Configure logging first
var isDevelopment = args.Contains("--environment=Development") || 
                   Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

if (isDevelopment)
{
    LoggingConfiguration.ConfigureDevelopmentLogging();
}
else
{
    LoggingConfiguration.ConfigureProductionLogging();
}

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog
    builder.Host.UseSerilog();

    // Add services
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    // Register DataAccess layer
    builder.Services.AddMoondeskDataAccess(connectionString);
    builder.Services.AddRepositories();

    // Add API services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
        Log.Information("Development environment detected - Swagger enabled");
    }

    // Initialize database (skip TimescaleDB for now)
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Moondesk.DataAccess.Data.MoondeskDbContext>();
        await context.Database.MigrateAsync();
        Log.Information("Database migrations applied successfully");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Database initialization failed");
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    
    // Map controllers and health endpoint
    app.MapControllers();
    app.MapGet("/health", () => new { Status = "Healthy", Timestamp = DateTime.UtcNow });

    Log.Information("Moondesk Host application starting on {Environment}...", app.Environment.EnvironmentName);
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
