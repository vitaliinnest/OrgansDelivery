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
    [Migration("20230320153421_ApplySeeds")]
    partial class ApplySeeds
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
                            Name = "manager"
                        },
                        new
                        {
                            Id = new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"),
                            Name = "employee"
                        },
                        new
                        {
                            Id = new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"),
                            Name = "admin"
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

            modelBuilder.Entity("OrgansDelivery.DAL.Entities.Invite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InviteCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("OrgansDelivery.DAL.Entities.Tenant", b =>
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

            modelBuilder.Entity("OrgansDelivery.DAL.Entities.User", b =>
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
                            ConcurrencyStamp = "79448760-bf6a-45e6-9c1c-3ee1597519d6",
                            Email = "CharlesHuber@medtronic.com",
                            EmailConfirmed = true,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Charles",
                            PasswordHash = "AQAAAAIAAYagAAAAEDtvy3SY1dKME2WfvgHjb4UHjXJuHif2eXk5jbLi30HOMbff47MW3UGYq9U1pbpRtw==",
                            PhoneNumberConfirmed = false,
                            Surname = "Huber",
                            TenantId = new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c870f62d-7ee0-4a21-8d5e-de19562915ea",
                            Email = "ReganLewis@medtronic.com",
                            EmailConfirmed = true,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Regan",
                            PasswordHash = "AQAAAAIAAYagAAAAEGKY6NGJW41VTp3MHOVflW7L4C57V+7/R0O5XSOW5vd5HcQvmNZkPQ3LSJ1rZOQUFQ==",
                            PhoneNumberConfirmed = false,
                            Surname = "Lewis",
                            TenantId = new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "10067289-a393-4262-834a-8e53edec2c90",
                            Email = "AndresBlake@bostoncorp.com",
                            EmailConfirmed = false,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Andres",
                            PasswordHash = "AQAAAAIAAYagAAAAEHdSyHhvugYoZFkcrejPTn1cTjsCOASPMuQVcmOdWw4NY8U6hbed4CtPMnu1OiLSug==",
                            PhoneNumberConfirmed = false,
                            Surname = "Blake",
                            TenantId = new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e45d052b-fd6c-4965-80ad-b575ecffcdca",
                            Email = "JavonSpencer@data.com",
                            EmailConfirmed = true,
                            Language = 0,
                            LockoutEnabled = false,
                            Name = "Javon",
                            PasswordHash = "AQAAAAIAAYagAAAAEKtpauWh+ctn9hTvggDLngtxZCIUTbsNHVIeBMP7x5eCxCVjhjS7F3rK0/jXQv+u/w==",
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
                    b.HasOne("OrgansDelivery.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("OrgansDelivery.DAL.Entities.User", null)
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

                    b.HasOne("OrgansDelivery.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("OrgansDelivery.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrgansDelivery.DAL.Entities.Invite", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
