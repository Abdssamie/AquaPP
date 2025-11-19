using Microsoft.EntityFrameworkCore;
using Moondesk.Domain.Models;
using Moondesk.Domain.Models.IoT;
using Moondesk.Domain.Models.Network;
using Moondesk.DataAccess.Models;

namespace Moondesk.DataAccess.Data;

public class MoondeskDbContext : DbContext
{
    public MoondeskDbContext(DbContextOptions<MoondeskDbContext> options) : base(options) { }

    // Core entities
    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationMembership> OrganizationMemberships { get; set; }
    public DbSet<ConnectionCredential> ConnectionCredentials { get; set; }

    // IoT entities with organization extensions
    public DbSet<AssetExtended> Assets { get; set; }
    public DbSet<SensorExtended> Sensors { get; set; }
    public DbSet<ReadingExtended> Readings { get; set; }
    public DbSet<AlertExtended> Alerts { get; set; }
    public DbSet<CommandExtended> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUserEntities(modelBuilder);
        ConfigureIoTEntities(modelBuilder);
        ConfigureIndexes(modelBuilder);
        ConfigureTimescaleDb(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // This will only be used if no options are provided
            optionsBuilder.UseNpgsql("Host=localhost;Database=moondesk;Username=postgres;Password=password");
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
    }

    private void ConfigureUserEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrganizationMembership>()
            .HasKey(om => new { om.UserId, om.OrganizationId });

        modelBuilder.Entity<OrganizationMembership>()
            .HasOne(om => om.User)
            .WithMany(u => u.Memberships)
            .HasForeignKey(om => om.UserId);

        modelBuilder.Entity<OrganizationMembership>()
            .HasOne(om => om.Organization)
            .WithMany(o => o.Memberships)
            .HasForeignKey(om => om.OrganizationId);
    }

    private void ConfigureIoTEntities(ModelBuilder modelBuilder)
    {
        // Only map extended entities, ignore base types
        modelBuilder.Ignore<Asset>();
        modelBuilder.Ignore<Sensor>();
        modelBuilder.Ignore<Reading>();
        modelBuilder.Ignore<Alert>();
        modelBuilder.Ignore<Command>();

        // Configure table names for extended entities
        modelBuilder.Entity<AssetExtended>().ToTable("assets");
        modelBuilder.Entity<SensorExtended>().ToTable("sensors");
        modelBuilder.Entity<ReadingExtended>().ToTable("readings");
        modelBuilder.Entity<AlertExtended>().ToTable("alerts");
        modelBuilder.Entity<CommandExtended>().ToTable("commands");
    }

    private void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        // User indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();

        // Organization indexes
        modelBuilder.Entity<Organization>()
            .HasIndex(o => o.OwnerId);

        // IoT performance indexes
        modelBuilder.Entity<AssetExtended>()
            .HasIndex(a => a.OrganizationId);

        modelBuilder.Entity<SensorExtended>()
            .HasIndex(s => new { s.OrganizationId, s.AssetId });
        modelBuilder.Entity<SensorExtended>()
            .HasIndex(s => new { s.OrganizationId, s.IsActive });

        // Critical reading indexes for TimescaleDB
        modelBuilder.Entity<ReadingExtended>()
            .HasIndex(r => new { r.OrganizationId, r.SensorId, r.Timestamp })
            .HasDatabaseName("IX_Readings_OrgId_SensorId_Timestamp");

        modelBuilder.Entity<ReadingExtended>()
            .HasIndex(r => new { r.OrganizationId, r.Timestamp })
            .HasDatabaseName("IX_Readings_OrgId_Timestamp");

        // Alert indexes
        modelBuilder.Entity<AlertExtended>()
            .HasIndex(a => new { a.OrganizationId, a.Acknowledged, a.Timestamp });

        // Command indexes
        modelBuilder.Entity<CommandExtended>()
            .HasIndex(c => new { c.OrganizationId, c.Status, c.CreatedAt });
        modelBuilder.Entity<CommandExtended>()
            .HasIndex(c => new { c.OrganizationId, c.UserId });
    }

    private void ConfigureTimescaleDb(ModelBuilder modelBuilder)
    {
        // Configure readings table for TimescaleDB hypertable
        modelBuilder.Entity<ReadingExtended>(entity =>
        {
            // Remove default primary key and create composite key with timestamp
            entity.HasKey(e => new { e.Id, e.Timestamp });
            
            // Fix timestamp column type to timestamptz
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("NOW()");
                
            // Optimize for time-series queries
            entity.Property(e => e.Value)
                .HasColumnType("double precision");
                
            // Add organization_id for space partitioning
            entity.Property(e => e.OrganizationId)
                .HasMaxLength(50)
                .IsRequired();
        });

        // Configure other timestamp columns
        modelBuilder.Entity<AlertExtended>()
            .Property(e => e.Timestamp)
            .HasColumnType("timestamptz");

        modelBuilder.Entity<AlertExtended>()
            .Property(e => e.AcknowledgedAt)
            .HasColumnType("timestamptz");
    }
}
