using Microsoft.EntityFrameworkCore;
using Moondesk.DataAccess.Data;

namespace Moondesk.DataAccess.Extensions;

public static class TimescaleDbExtensions
{
    public static async Task ConfigureTimescaleDbAsync(this MoondeskDbContext context)
    {
        try
        {
            // Check if TimescaleDB extension exists
            var hasTimescale = await context.Database.ExecuteSqlRawAsync(@"
                SELECT 1 FROM pg_extension WHERE extname = 'timescaledb';
            ");

            // Create TimescaleDB hypertable for readings
            await context.Database.ExecuteSqlRawAsync(@"
                SELECT create_hypertable('readings', 'timestamp', 
                    chunk_time_interval => INTERVAL '1 day',
                    partitioning_column => 'organization_id',
                    number_partitions => 4,
                    if_not_exists => TRUE
                );
            ");

            // Create continuous aggregates for common queries
            await context.Database.ExecuteSqlRawAsync(@"
                CREATE MATERIALIZED VIEW IF NOT EXISTS readings_hourly
                WITH (timescaledb.continuous) AS
                SELECT 
                    time_bucket('1 hour', timestamp) AS bucket,
                    organization_id,
                    sensor_id,
                    AVG(value) as avg_value,
                    MIN(value) as min_value,
                    MAX(value) as max_value,
                    COUNT(*) as reading_count
                FROM readings
                GROUP BY bucket, organization_id, sensor_id
                WITH NO DATA;
            ");

            // Add retention policy (keep raw data for 30 days)
            await context.Database.ExecuteSqlRawAsync(@"
                SELECT add_retention_policy('readings', INTERVAL '30 days', if_not_exists => TRUE);
            ");

            // Add compression policy
            await context.Database.ExecuteSqlRawAsync(@"
                ALTER TABLE readings SET (
                    timescaledb.compress,
                    timescaledb.compress_segmentby = 'organization_id, sensor_id'
                );
                SELECT add_compression_policy('readings', INTERVAL '7 days', if_not_exists => TRUE);
            ");
        }
        catch (Exception ex)
        {
            // Log warning but don't fail startup if TimescaleDB is not available
            Console.WriteLine($"Warning: TimescaleDB configuration failed: {ex.Message}");
            Console.WriteLine("Application will continue with regular PostgreSQL tables.");
        }
    }
}
