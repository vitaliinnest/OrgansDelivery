using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Extensions;
using OrganStorage.DAL.Services;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public Guid TenantId { get; set; }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Conditions> Conditions { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Organ> Organs { get; set; }
    public DbSet<ConditionsRecord> Records { get; set; }
    public DbSet<Device> Devices { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.SetTenantIdIfNeeded(TenantId);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        this.SetTenantIdIfNeeded(TenantId);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // todo: configure on delete actions

        builder.Entity<Container>(container =>
        {
            container
                .HasOne(c => c.Organ)
                .WithOne(o => o.Container)
                .HasForeignKey<Organ>(o => o.ContainerId)
                .OnDelete(DeleteBehavior.SetNull);

			container
				.HasOne(c => c.Device)
				.WithOne(d => d.Container)
				.HasForeignKey<Device>(c => c.ContainerId)
				.OnDelete(DeleteBehavior.SetNull);

			container
                .HasMany(c => c.Records)
                .WithOne(r => r.Container)
                .HasForeignKey(r => r.ContainerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Conditions>(conditions =>
        {
			conditions
				.HasMany(p => p.Containers)
				.WithOne(c => c.Conditions);

			conditions.OwnsOne(p => p.Humidity);
            conditions.Navigation(c => c.Humidity).IsRequired();

            conditions.OwnsOne(p => p.Light);
            conditions.Navigation(c => c.Light).IsRequired();

            conditions.OwnsOne(p => p.Temperature);
            conditions.Navigation(c => c.Temperature).IsRequired();

            conditions.OwnsOne(p => p.Orientation);
            conditions.Navigation(c => c.Orientation).IsRequired();
        });

        builder.Entity<Device>(device =>
        {
            device
                .HasMany(d => d.Records)
                .WithOne(r => r.Device)
				.HasForeignKey(r => r.DeviceId)
				.OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<ConditionsRecord>(record =>
        {
            record.OwnsOne(r => r.Orientation);
            record.Navigation(e => e.Orientation).IsRequired();
        });

        AddTenantQueryFilter(builder);

        ConvertDateTimeUtcFormat(builder);
        
        SetDecimalPrecision(builder);

        AppDbContextSeed.SeedData(builder);
    }

    private static void SetDecimalPrecision(ModelBuilder builder)
    {
        var decimalProps = builder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

        foreach (var property in decimalProps)
        {
            property.SetPrecision(18);
            property.SetScale(2);
        }
    }

    private static void ConvertDateTimeUtcFormat(ModelBuilder builder)
    {
        var properties = builder.Model.GetEntityTypes()
            .Where(e => !e.IsKeyless)
            .SelectMany(entityType => entityType.GetProperties());

        foreach (var property in properties)
        {
            if (property.ClrType == typeof(DateTime))
            {
                property.SetValueConverter(ValueConverterBuilder.BuildDateTimeConverter());
            }
            else if (property.ClrType == typeof(DateTime?))
            {
                property.SetValueConverter(ValueConverterBuilder.BuildNullableDateTimeConverter());
            }
        }
    }

    private void AddTenantQueryFilter(ModelBuilder builder)
    {
        var mustHaveTenantTypes = builder.Model.GetEntityTypes()
            .Where(entityType => typeof(IMustHaveTenant)
            .IsAssignableFrom(entityType.ClrType));
        
        PrintTenantId();

        foreach (var entityType in mustHaveTenantTypes)
        {
            entityType.AddTenantQueryFilter(TenantId);
        }
    }

    private void PrintTenantId()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"TenantId: {TenantId}");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine();
    }
}
