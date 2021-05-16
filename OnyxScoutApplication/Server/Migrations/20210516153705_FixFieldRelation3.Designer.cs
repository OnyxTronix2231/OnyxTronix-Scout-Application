﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnyxScoutApplication.Server.Data;

namespace OnyxScoutApplication.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210516153705_FixFieldRelation3")]
    partial class FixFieldRelation3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
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

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OnyxScoutApplication.Server.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

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

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CustomAlliance");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliances", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlueId")
                        .HasColumnType("int");

                    b.Property<int?>("RedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlueId");

                    b.HasIndex("RedId");

                    b.ToTable("CustomAlliances");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlliancesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatchNumber")
                        .HasColumnType("int");

                    b.Property<string>("WinningAlliance")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlliancesId");

                    b.HasIndex("EventId");

                    b.ToTable("CustomMatch");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomAllianceId")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomAllianceId");

                    b.ToTable("CustomTeam");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowManualInput")
                        .HasColumnType("bit");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FieldId")
                        .HasColumnType("int");

                    b.Property<int?>("FieldStageId")
                        .HasColumnType("int");

                    b.Property<int>("FieldType")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int>("MaxValue")
                        .HasColumnType("int");

                    b.Property<int>("MaximumSelectionLength")
                        .HasColumnType("int");

                    b.Property<int>("MinValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Required")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("FieldStageId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.FieldsInStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScoutFormFormatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScoutFormFormatId");

                    b.ToTable("FieldsInStage");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PercentWeight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.ScoutFormFormat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ScoutFormFormats");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MatchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamNumber")
                        .HasColumnType("int");

                    b.Property<string>("WriterUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ScoutForms");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<int?>("FormDataId")
                        .HasColumnType("int");

                    b.Property<int?>("FormDataStageId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("FormDataId");

                    b.HasIndex("FormDataStageId");

                    b.ToTable("FormData");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormDataInStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FormId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormDataInStage");
                });

            modelBuilder.Entity("OnyxScoutApplication.Server.Models.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasDiscriminator().HasValue("ApplicationRole");
                });

            modelBuilder.Entity("OnyxScoutApplication.Server.Models.ApplicationUserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<string>");

                    b.Property<string>("ApplicationRoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("ApplicationRoleId");

                    b.HasDiscriminator().HasValue("ApplicationUserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OnyxScoutApplication.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnyxScoutApplication.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnyxScoutApplication.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliances", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliance", "Blue")
                        .WithMany()
                        .HasForeignKey("BlueId");

                    b.HasOne("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliance", "Red")
                        .WithMany()
                        .HasForeignKey("RedId");

                    b.Navigation("Blue");

                    b.Navigation("Red");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomMatch", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliances", "Alliances")
                        .WithMany()
                        .HasForeignKey("AlliancesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomEvent", "Event")
                        .WithMany("Matches")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alliances");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomTeam", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliance", null)
                        .WithMany("Teams")
                        .HasForeignKey("CustomAllianceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Field", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Field", "ParentField")
                        .WithMany("CascadeFields")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.FieldsInStage", "FieldsInStage")
                        .WithMany("Fields")
                        .HasForeignKey("FieldStageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("FieldsInStage");

                    b.Navigation("ParentField");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.FieldsInStage", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.ScoutFormFormat", "ScoutFormFormat")
                        .WithMany("FieldsInStages")
                        .HasForeignKey("ScoutFormFormatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScoutFormFormat");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Option", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Field", null)
                        .WithMany("Options")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormData", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormData", null)
                        .WithMany("CascadeData")
                        .HasForeignKey("FormDataId");

                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormDataInStage", "FormDataInStage")
                        .WithMany("FormData")
                        .HasForeignKey("FormDataStageId");

                    b.Navigation("Field");

                    b.Navigation("FormDataInStage");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormDataInStage", b =>
                {
                    b.HasOne("OnyxScoutApplication.Shared.Models.ScoutFormModels.Form", "Form")
                        .WithMany("FormDataInStages")
                        .HasForeignKey("FormId");

                    b.Navigation("Form");
                });

            modelBuilder.Entity("OnyxScoutApplication.Server.Models.ApplicationUserRole", b =>
                {
                    b.HasOne("OnyxScoutApplication.Server.Models.ApplicationRole", "Role")
                        .WithMany()
                        .HasForeignKey("ApplicationRoleId");

                    b.HasOne("OnyxScoutApplication.Server.Models.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnyxScoutApplication.Server.Models.ApplicationUser", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomAlliance", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.CustomeEventModels.CustomEvent", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.Field", b =>
                {
                    b.Navigation("CascadeFields");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.FieldsInStage", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormFormatModels.ScoutFormFormat", b =>
                {
                    b.Navigation("FieldsInStages");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.Form", b =>
                {
                    b.Navigation("FormDataInStages");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormData", b =>
                {
                    b.Navigation("CascadeData");
                });

            modelBuilder.Entity("OnyxScoutApplication.Shared.Models.ScoutFormModels.FormDataInStage", b =>
                {
                    b.Navigation("FormData");
                });
#pragma warning restore 612, 618
        }
    }
}
