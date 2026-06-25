using Microsoft.EntityFrameworkCore;
using Commun.Data.Entities;

namespace Commun.Data;

public class CommunDbContext : DbContext
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<ConnectionHistory> ConnectionHistory { get; set; }
    public DbSet<DataRecord> DataRecords { get; set; }

    public CommunDbContext(DbContextOptions<CommunDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Protocol).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<ConnectionHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Device)
                .WithMany(d => d.ConnectionHistory)
                .HasForeignKey(e => e.DeviceId);
        });

        modelBuilder.Entity<DataRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Device)
                .WithMany(d => d.DataRecords)
                .HasForeignKey(e => e.DeviceId);
            entity.HasIndex(e => e.Timestamp);
        });
    }
}
