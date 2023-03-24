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

    // todo: replace with set only
    public Guid TenantId { get; set; }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Conditions> Conditions { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Organ> Organs { get; set; }
    public DbSet<ConditionsRecord> ConditionsHistory { get; set; }

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
        // todo: fix warnings

        builder.Entity<Container>()
            .HasOne(c => c.Organ)
            .WithOne(o => o.Container)
            .HasForeignKey<Organ>(o => o.ContainerId);

        builder.Entity<Container>()
            .HasMany(c => c.ConditionsHistory)
            .WithOne(r => r.Container);

        builder.Entity<Conditions>()
            .HasMany(p => p.Containers)
            .WithOne(c => c.Conditions);

        builder.Entity<Conditions>(conds =>
        {
            conds.OwnsOne(p => p.Humidity);
            conds.Navigation(c => c.Humidity).IsRequired();

            conds.OwnsOne(p => p.Light);
            conds.Navigation(c => c.Light).IsRequired();

            conds.OwnsOne(p => p.Temperature);
            conds.Navigation(c => c.Temperature).IsRequired();

            conds.OwnsOne(p => p.Orientation);
            conds.Navigation(c => c.Orientation).IsRequired();
        });

        builder.Entity<ConditionsRecord>(record =>
        {
            record.OwnsOne(r => r.Orientation);
            record.Navigation(e => e.Orientation).IsRequired();
        });

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

        Console.WriteLine(new string('-', 10));
        Console.WriteLine($"TenantId: {TenantId}");

        foreach (var entityType in mustHaveTenantTypes)
        {
            Console.WriteLine($"EntityType: {entityType.Name}");
            entityType.AddTenantQueryFilter(TenantId);
        }

        Console.WriteLine(new string('-', 10));
    }
}
