using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Entities;
using OrganStorage.DAL.Enums;

namespace OrganStorage.DAL.Data;

public static class AppDbContextSeed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        SeedTenants(modelBuilder);
        SeedUsers(modelBuilder);
        SeedRoles(modelBuilder);
        SeedUserRoles(modelBuilder);
    }

    private static void SeedTenants(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>().HasData(
            new()
            {
                Id = TenantConsts.MEDTRONIC_TENANT_ID,
                Name = "Medtronic",
            },
            new()
            {
                Id = TenantConsts.BOSTON_TENANT_ID,
                Name = "Boston Scientific Corporation",
            }
        );
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher<User>();

        modelBuilder.Entity<User>().HasData(
            new()
            {
                Id = UserConsts.MEDTRONIC_MANAGER_USER_ID,
                TenantId = TenantConsts.MEDTRONIC_TENANT_ID,
                Name = "Charles",
                Surname = "Huber",
                Email = "CharlesHuber@medtronic.com",
                NormalizedEmail = "CharlesHuber@medtronic.com".ToUpper(),
                EmailConfirmed = true,
                Language = Language.English,
                PasswordHash = passwordHasher.HashPassword(user: null, "CharlesHuber123"),
            },
            new()
            {
                Id = UserConsts.MEDTRONIC_WORKER_USER_ID,
                TenantId = TenantConsts.MEDTRONIC_TENANT_ID,
                Name = "Regan",
                Surname = "Lewis",
                Email = "ReganLewis@medtronic.com",
                NormalizedEmail = "ReganLewis@medtronic.com".ToUpper(),
                EmailConfirmed = true,
                Language = Language.English,
                PasswordHash = passwordHasher.HashPassword(user: null, "ReganLewis123"),
            },
            new()
            {
                Id = UserConsts.BOSTON_MANAGER_ID,
                TenantId = TenantConsts.BOSTON_TENANT_ID,
                Name = "Andres",
                Surname = "Blake",
                Email = "AndresBlake@bostoncorp.com",
                NormalizedEmail = "AndresBlake@bostoncorp.com".ToUpper(),
                Language = Language.English,
                PasswordHash = passwordHasher.HashPassword(user: null, "AndresBlake123"),
            },
            new()
            {
                Id = UserConsts.ADMIN_USER_ID,
                Name = "Javon",
                Surname = "Spencer",
                Email = "JavonSpencer@data.com",
                NormalizedEmail = "JavonSpencer@data.com".ToUpper(),
                EmailConfirmed = true,
                Language = Language.English,
                PasswordHash = passwordHasher.HashPassword(user: null, "JavonSpencer123"),
            }
        );
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new()
            {
                Id = RoleConsts.USER_ID,
                Name = UserRoles.USER,
                NormalizedName = UserRoles.USER.ToUpper(),
            },
            new()
            {
                Id = RoleConsts.ADMIN_ID,
                Name = UserRoles.ADMIN,
                NormalizedName = UserRoles.ADMIN.ToUpper(),
            }
        );
    }

    private static void SeedUserRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new()
            {
                UserId = UserConsts.MEDTRONIC_MANAGER_USER_ID,
                RoleId = RoleConsts.USER_ID,
            },
            new()
            {
                UserId = UserConsts.MEDTRONIC_WORKER_USER_ID,
                RoleId = RoleConsts.USER_ID,
            },
            new()
            {
                UserId = UserConsts.BOSTON_MANAGER_ID,
                RoleId = RoleConsts.USER_ID,
            },
            new()
            {
                UserId = UserConsts.ADMIN_USER_ID,
                RoleId = RoleConsts.ADMIN_ID,
            }
        );
    }

    private static class TenantConsts
    {
        public static readonly Guid MEDTRONIC_TENANT_ID = new("a906297e-d95d-4116-b479-e5e3f323b26d");
        public static readonly Guid BOSTON_TENANT_ID = new("f4c76e71-6a6c-476a-9d15-328a09447646");
    }

    private static class RoleConsts
    {
        public static readonly Guid USER_ID = new("afab5690-50ea-4043-bf7b-0f825f068b94");
        public static readonly Guid ADMIN_ID = new("ca8531d7-5592-4678-80d2-aecdcb462208");
    }

    private static class UserConsts
    {
        public static readonly Guid MEDTRONIC_MANAGER_USER_ID = new("da5bec95-daf8-41d6-b980-69091c89532a");
        public static readonly Guid MEDTRONIC_WORKER_USER_ID = new("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320");
        public static readonly Guid BOSTON_MANAGER_ID = new("f7355727-a103-41fe-a1a8-a543b9682605");
        public static readonly Guid ADMIN_USER_ID = new("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a");
    }
}
