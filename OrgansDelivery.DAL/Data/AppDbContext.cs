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
    public readonly bool AdminMode;
    public readonly Guid TenantId;
    //private readonly ILogger<AppDbContext> _logger;
    
	public AppDbContext(IEnvironmentProvider environmentProvider, DbContextOptions options) : base(options)
    {
		AdminMode = environmentProvider.User?.Id == Consts.Consts.ADMIN_ID;
		TenantId = environmentProvider.Tenant?.Id ?? Guid.Empty;
		//_logger = logger;
	}

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

	public TEntity DetachedClone<TEntity>(TEntity entity) where TEntity : class
		=> Entry(entity).CurrentValues.Clone().ToObject() as TEntity;

	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Organ>(organ =>
        {
            organ
                .HasOne(o => o.Conditions)
                .WithMany(c => c.Organs)
                .OnDelete(DeleteBehavior.Restrict);

			organ
			    .HasOne(o => o.Container)
		        .WithOne(c => c.Organ)
		        .HasForeignKey<Organ>(o => o.ContainerId)
				.OnDelete(DeleteBehavior.Restrict);
		});

        builder.Entity<Container>(container =>
        {
            container
                .HasOne(c => c.Device)
                .WithOne(d => d.Container)
                .HasForeignKey<Container>(c => c.DeviceId)
				.OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Conditions>(conditions =>
        {
			conditions
                .OwnsOne(p => p.Humidity);
            conditions
                .Navigation(c => c.Humidity)
                .IsRequired();

            conditions
                .OwnsOne(p => p.Light);
            conditions
                .Navigation(c => c.Light)
                .IsRequired();

            conditions
                .OwnsOne(p => p.Temperature);
            conditions
                .Navigation(c => c.Temperature)
                .IsRequired();

            conditions
                .OwnsOne(p => p.Orientation);
            conditions
                .Navigation(c => c.Orientation)
                .IsRequired();
        });

        builder.Entity<ConditionsRecord>(record =>
        {
            record
                .HasOne(r => r.Organ)
                .WithMany(o => o.Records)
                .OnDelete(DeleteBehavior.Cascade);

            record
                .HasOne(r => r.Conditions)
                .WithMany(c => c.Records)
                .OnDelete(DeleteBehavior.Cascade);

            record
                .OwnsOne(r => r.Orientation);
            
            record
                .Navigation(e => e.Orientation)
                .IsRequired();
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
        
        LogTenantId();

        foreach (var entityType in mustHaveTenantTypes)
        {
            entityType.AddTenantQueryFilter(TenantId, AdminMode);
        }
    }

    private void LogTenantId()
    {
        //_logger.LogInformation($"TenantId: {TenantId}");
    }
}
