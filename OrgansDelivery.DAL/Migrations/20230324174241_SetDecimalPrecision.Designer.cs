﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrganStorage.DAL.Data;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230324174241_SetDecimalPrecision")]
    partial class SetDecimalPrecision
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"),
                            Name = "manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"),
                            Name = "worker",
                            NormalizedName = "WORKER"
                        },
                        new
                        {
                            Id = new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"),
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                            RoleId = new Guid("afab5690-50ea-4043-bf7b-0f825f068b94")
                        },
                        new
                        {
                            UserId = new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                            RoleId = new Guid("afd73abe-c965-44b6-a5ac-42a728507f68")
                        },
                        new
                        {
                            UserId = new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                            RoleId = new Guid("afab5690-50ea-4043-bf7b-0f825f068b94")
                        },
                        new
                        {
                            UserId = new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                            RoleId = new Guid("ca8531d7-5592-4678-80d2-aecdcb462208")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Conditions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Conditions");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.ConditionsRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Humidity")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Light")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Temperature")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.ToTable("ConditionsHistory");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Container", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConditionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConditionsIntervalCheckInSecs")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrganId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ConditionsId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Invite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InviteCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Organ", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContainerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrganCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId")
                        .IsUnique()
                        .HasFilter("[ContainerId] IS NOT NULL");

                    b.ToTable("Organs");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"),
                            Description = "Better health access for our world by combining the powerof healthcare technology with strong partnerships,we are helping put equity within reach",
                            Name = "Medtronic",
                            Url = "medtronic"
                        },
                        new
                        {
                            Id = new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"),
                            Description = "A Boston Scientific device brought Caroline relief – and the inspiration to pursue medicine",
                            Name = "Boston Scientific Corporation",
                            Url = "bostoncorp"
                        });
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4aa3d220-9812-4dab-8c79-6867a72011d0",
                            Email = "CharlesHuber@medtronic.com",
                            EmailConfirmed = true,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Charles",
                            PasswordHash = "AQAAAAIAAYagAAAAEAjt3zEGu45Uc0QdysJ9NgOOAiRbi6ZEPui1LGDzTb+oQ4G6lmNq4YmDnMfYv2a25w==",
                            PhoneNumberConfirmed = false,
                            Surname = "Huber",
                            TenantId = new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a2ecee72-9b22-493f-9fb3-89e1147bcdf0",
                            Email = "ReganLewis@medtronic.com",
                            EmailConfirmed = true,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Regan",
                            PasswordHash = "AQAAAAIAAYagAAAAEGnk4d2Nx8li3U5ZIBqitObufvqVCXNa4+gOXzu/u+8GwQkg40zBuQkVfv6K3VpzZw==",
                            PhoneNumberConfirmed = false,
                            Surname = "Lewis",
                            TenantId = new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3a553c79-6173-4fc9-b2f3-5533b8b7ab60",
                            Email = "AndresBlake@bostoncorp.com",
                            EmailConfirmed = false,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Andres",
                            PasswordHash = "AQAAAAIAAYagAAAAEBHGli/BzXhKWwiVAB88BXCNVgOM2KxbXBGUipcLxmq87UYJsyWrwVaEGduz2uA1tg==",
                            PhoneNumberConfirmed = false,
                            Surname = "Blake",
                            TenantId = new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "76e162e0-633a-4320-98b4-da89cd405476",
                            Email = "JavonSpencer@data.com",
                            EmailConfirmed = true,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Javon",
                            PasswordHash = "AQAAAAIAAYagAAAAEOYZfOIsYVqi1ClWoVAzGkeC/I0ZemJBydziT5K1piXKHRr+9TDui8iBcH8KSplVxA==",
                            PhoneNumberConfirmed = false,
                            Surname = "Spencer",
                            TenantId = new Guid("00000000-0000-0000-0000-000000000000"),
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("OrganStorage.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("OrganStorage.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrganStorage.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("OrganStorage.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Conditions", b =>
                {
                    b.OwnsOne("OrganStorage.DAL.Entities.Condition<OrganStorage.DAL.Entities.Orientation>", "Orientation", b1 =>
                        {
                            b1.Property<Guid>("ConditionsId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ConditionsId");

                            b1.ToTable("Conditions");

                            b1.WithOwner()
                                .HasForeignKey("ConditionsId");

                            b1.OwnsOne("OrganStorage.DAL.Entities.Orientation", "AllowedDeviation", b2 =>
                                {
                                    b2.Property<Guid>("ConditionsId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("X")
                                        .HasPrecision(18, 2)
                                        .HasColumnType("decimal(18,2)");

                                    b2.Property<decimal>("Y")
                                        .HasPrecision(18, 2)
                                        .HasColumnType("decimal(18,2)");

                                    b2.HasKey("ConditionsId");

                                    b2.ToTable("Conditions");

                                    b2.WithOwner()
                                        .HasForeignKey("ConditionsId");
                                });

                            b1.OwnsOne("OrganStorage.DAL.Entities.Orientation", "ExpectedValue", b2 =>
                                {
                                    b2.Property<Guid>("ConditionsId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("X")
                                        .HasPrecision(18, 2)
                                        .HasColumnType("decimal(18,2)");

                                    b2.Property<decimal>("Y")
                                        .HasPrecision(18, 2)
                                        .HasColumnType("decimal(18,2)");

                                    b2.HasKey("ConditionsId");

                                    b2.ToTable("Conditions");

                                    b2.WithOwner()
                                        .HasForeignKey("ConditionsId");
                                });

                            b1.Navigation("AllowedDeviation");

                            b1.Navigation("ExpectedValue");
                        });

                    b.OwnsOne("OrganStorage.DAL.Entities.Condition<decimal>", "Humidity", b1 =>
                        {
                            b1.Property<Guid>("ConditionsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("AllowedDeviation")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.Property<decimal>("ExpectedValue")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ConditionsId");

                            b1.ToTable("Conditions");

                            b1.WithOwner()
                                .HasForeignKey("ConditionsId");
                        });

                    b.OwnsOne("OrganStorage.DAL.Entities.Condition<decimal>", "Light", b1 =>
                        {
                            b1.Property<Guid>("ConditionsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("AllowedDeviation")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.Property<decimal>("ExpectedValue")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ConditionsId");

                            b1.ToTable("Conditions");

                            b1.WithOwner()
                                .HasForeignKey("ConditionsId");
                        });

                    b.OwnsOne("OrganStorage.DAL.Entities.Condition<decimal>", "Temperature", b1 =>
                        {
                            b1.Property<Guid>("ConditionsId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("AllowedDeviation")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.Property<decimal>("ExpectedValue")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ConditionsId");

                            b1.ToTable("Conditions");

                            b1.WithOwner()
                                .HasForeignKey("ConditionsId");
                        });

                    b.Navigation("Humidity")
                        .IsRequired();

                    b.Navigation("Light")
                        .IsRequired();

                    b.Navigation("Orientation")
                        .IsRequired();

                    b.Navigation("Temperature")
                        .IsRequired();
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.ConditionsRecord", b =>
                {
                    b.HasOne("OrganStorage.DAL.Entities.Container", "Container")
                        .WithMany("ConditionsHistory")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("OrganStorage.DAL.Entities.Orientation", "Orientation", b1 =>
                        {
                            b1.Property<Guid>("ConditionsRecordId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("X")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.Property<decimal>("Y")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ConditionsRecordId");

                            b1.ToTable("ConditionsHistory");

                            b1.WithOwner()
                                .HasForeignKey("ConditionsRecordId");
                        });

                    b.Navigation("Container");

                    b.Navigation("Orientation")
                        .IsRequired();
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Container", b =>
                {
                    b.HasOne("OrganStorage.DAL.Entities.Conditions", "Conditions")
                        .WithMany("Containers")
                        .HasForeignKey("ConditionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conditions");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Organ", b =>
                {
                    b.HasOne("OrganStorage.DAL.Entities.Container", "Container")
                        .WithOne("Organ")
                        .HasForeignKey("OrganStorage.DAL.Entities.Organ", "ContainerId");

                    b.Navigation("Container");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Conditions", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("OrganStorage.DAL.Entities.Container", b =>
                {
                    b.Navigation("ConditionsHistory");

                    b.Navigation("Organ");
                });
#pragma warning restore 612, 618
        }
    }
}
