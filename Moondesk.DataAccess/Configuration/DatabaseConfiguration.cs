using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moondesk.DataAccess.Data;
using Moondesk.DataAccess.Extensions;

namespace Moondesk.DataAccess.Configuration;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddMoondeskDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MoondeskDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(3);
                npgsqlOptions.CommandTimeout(30);
            });
            
            // Enable snake_case naming convention
            options.UseSnakeCaseNamingConvention();
            
            // Enable sensitive data logging in development
            #if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
            #endif
        });

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MoondeskDbContext>();
        
        // Apply migrations
        await context.Database.MigrateAsync();
        
        // Configure TimescaleDB
        await context.ConfigureTimescaleDbAsync();
    }
}
