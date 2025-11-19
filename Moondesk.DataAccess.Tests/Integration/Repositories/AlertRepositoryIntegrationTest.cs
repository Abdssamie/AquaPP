using Microsoft.EntityFrameworkCore;
using Moondesk.DataAccess.Data;
using Moondesk.DataAccess.Tests.Fixtures;

namespace Moondesk.DataAccess.Tests.Integration.Repositories;

public class AlertRepositoryIntegrationTest : IClassFixture<TimescaleDbTestContainerFixture>
{
    private readonly TimescaleDbTestContainerFixture _fixture;

    public AlertRepositoryIntegrationTest(TimescaleDbTestContainerFixture fixture)
    {
        _fixture = fixture;
    }

    private MoondeskDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MoondeskDbContext>()
            .UseNpgsql(_fixture.ConnectionString)
            .Options;

        var context = new MoondeskDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

}