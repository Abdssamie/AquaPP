using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Moondesk.DataAccess.Data;

public class MoondeskDbContextFactory : IDesignTimeDbContextFactory<MoondeskDbContext>
{
    public MoondeskDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MoondeskDbContext>();
        
        // Load configuration from appsettings
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Host=localhost;Database=moondesk;Username=postgres;Password=password";
        
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        
        return new MoondeskDbContext(optionsBuilder.Options);
    }
}
