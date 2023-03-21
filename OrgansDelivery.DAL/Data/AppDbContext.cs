using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrgansDelivery.DAL.Entities;
using OrgansDelivery.DAL.Services;
using OrgansDelivery.DAL.Interfaces;
using OrgansDelivery.DAL.Extensions;
namespace OrgansDelivery.DAL.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly Guid _tenantId;

    public AppDbContext(
        DbContextOptions options,
        IEnvironmentProvider environmentProvider
        ) : base(options)
    {
        _tenantId = environmentProvider.Tenant?.Id ?? Guid.Empty;
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Invite> Invites { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.SetTenantIdIfNeeded(_tenantId);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        this.SetTenantIdIfNeeded(_tenantId);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        AddTenantQueryFilter(builder);
        
        ConvertDateTimeUtcFormat(builder);

        AppDbContextSeed.SeedData(builder);
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

        foreach (var entityType in mustHaveTenantTypes)
        {
            entityType.AddTenantQueryFilter(_tenantId);
        }
    }
}
