﻿// <auto-generated />
using System;
using Covid_Project.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Covid_Project.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210515071759_InitialDB")]
    partial class InitialDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Covid_Project.Domain.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DynamicCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("IsVerified")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.AccountHasPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("AccountHasPermission");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.AccountHasRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("AccountHasRole");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedById");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.DailyCheckin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSymptomatic")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("DailyCheckin");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Itinerary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureCityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinationCityId")
                        .HasColumnType("int");

                    b.Property<string>("FlyNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LandingTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DepartureCityId");

                    b.HasIndex("DestinationCityId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("UserItinerary");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.LocationCheckin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("LocationCheckin");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.MedicalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<bool>("Asthma")
                        .HasColumnType("bit");

                    b.Property<bool>("Cough")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Fever")
                        .HasColumnType("bit");

                    b.Property<bool>("HIV")
                        .HasColumnType("bit");

                    b.Property<bool>("HeartProblem")
                        .HasColumnType("bit");

                    b.Property<bool>("HighBloodPressure")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("None")
                        .HasColumnType("bit");

                    b.Property<bool>("Obesity")
                        .HasColumnType("bit");

                    b.Property<bool>("Pregnancy")
                        .HasColumnType("bit");

                    b.Property<bool>("RunningNose")
                        .HasColumnType("bit");

                    b.Property<bool>("ShortnessOfBreath")
                        .HasColumnType("bit");

                    b.Property<string>("SpecialSymptom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Tiredness")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("UserMedicalInfo");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedById");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.PeopleLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LocationCheckinId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationCheckinId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("PeopleLocation");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuardName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("IdNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nationality")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("UpdatedById");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuardName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.RoleHasPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("RoleHasPermission");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Testing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Result")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TestingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TestingLocationId")
                        .HasColumnType("int");

                    b.Property<int>("TestingState")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TestingLocationId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Testing");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.TestingLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("TestingLocation");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Account", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.AccountHasPermission", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("AccountHasPermissions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Permission", "Permission")
                        .WithMany("AccountHasPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("Permission");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.AccountHasRole", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("AccountHasRoles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Role", "Role")
                        .WithMany("AccountHasRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("Role");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.City", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.DailyCheckin", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("DailyCheckins")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Itinerary", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("Itineraries")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.City", "DepartureCity")
                        .WithMany("DepartureItineraries")
                        .HasForeignKey("DepartureCityId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.City", "DestinationCity")
                        .WithMany("DestinationItineraties")
                        .HasForeignKey("DestinationCityId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("DepartureCity");

                    b.Navigation("DestinationCity");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.LocationCheckin", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("LocationCheckins")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.MedicalInfo", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("MedicalInfo")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.News", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.PeopleLocation", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.LocationCheckin", "LocationCheckin")
                        .WithMany("PeopleLocations")
                        .HasForeignKey("LocationCheckinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("LocationCheckin");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Permission", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Profile", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithOne("Profile")
                        .HasForeignKey("Covid_Project.Domain.Models.Profile", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Role", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.RoleHasPermission", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Permission", "Permission")
                        .WithMany("RoleHasPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Role", "Role")
                        .WithMany("RoleHasPermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Permission");

                    b.Navigation("Role");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Testing", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.Account", "Account")
                        .WithMany("Testings")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.TestingLocation", "TestingLocation")
                        .WithMany("Testings")
                        .HasForeignKey("TestingLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Account");

                    b.Navigation("TestingLocation");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.TestingLocation", b =>
                {
                    b.HasOne("Covid_Project.Domain.Models.City", "City")
                        .WithMany("TestingLocations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Covid_Project.Domain.Models.Account", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("City");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Account", b =>
                {
                    b.Navigation("AccountHasPermissions");

                    b.Navigation("AccountHasRoles");

                    b.Navigation("DailyCheckins");

                    b.Navigation("Itineraries");

                    b.Navigation("LocationCheckins");

                    b.Navigation("MedicalInfo");

                    b.Navigation("Profile");

                    b.Navigation("Testings");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.City", b =>
                {
                    b.Navigation("DepartureItineraries");

                    b.Navigation("DestinationItineraties");

                    b.Navigation("TestingLocations");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.LocationCheckin", b =>
                {
                    b.Navigation("PeopleLocations");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Permission", b =>
                {
                    b.Navigation("AccountHasPermissions");

                    b.Navigation("RoleHasPermissions");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.Role", b =>
                {
                    b.Navigation("AccountHasRoles");

                    b.Navigation("RoleHasPermissions");
                });

            modelBuilder.Entity("Covid_Project.Domain.Models.TestingLocation", b =>
                {
                    b.Navigation("Testings");
                });
#pragma warning restore 612, 618
        }
    }
}
