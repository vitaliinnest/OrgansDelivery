﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Extensions;
using OrganStorage.DAL.Services;
using OrganStorage.DAL.Interfaces;

namespace OrganStorage.DAL.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IEnvironmentProvider _environmentProvider;

    public AppDbContext(
        DbContextOptions options,
        IEnvironmentProvider environmentProvider
        ) : base(options)
    {
        _environmentProvider = environmentProvider;
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<ConditionsPreset> ConditionPresets { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Organ> Organs { get; set; }
    public DbSet<ContainerConditionsRecord> ConditionsHistory { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var tenantId = GetTenantIdOrDefaultValue();
        this.SetTenantIdIfNeeded(tenantId);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantIdOrDefaultValue();
        this.SetTenantIdIfNeeded(tenantId);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Container>()
            .HasOne(c => c.Organ)
            .WithOne(o => o.Container)
            .HasForeignKey<Organ>(o => o.ContainerId);

        builder.Entity<Container>()
            .HasMany(c => c.ConditionsHistory)
            .WithOne(r => r.Container);

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

        var tenantId = GetTenantIdOrDefaultValue();

        foreach (var entityType in mustHaveTenantTypes)
        {
            entityType.AddTenantQueryFilter(tenantId);
        }
    }

    private Guid GetTenantIdOrDefaultValue()
    {
        return _environmentProvider.Tenant?.Id ?? Guid.Empty;
    }
}
