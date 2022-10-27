using System;
using System.Threading;
using System.Threading.Tasks;
using Covid_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Covid_Project.Persistence.Context
{
    public class AppDbContext : DbContext
    {       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<AccountHasPermission> AccountHasPermissions { get; set; }
        public DbSet<AccountHasRole> AccountHasRoles { get; set; }
        public DbSet<DailyCheckin> DailyCheckins { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<LocationCheckin> LocationCheckins { get; set; }
        public DbSet<MedicalInfo> MedicalInfo { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PeopleLocation> PeopleLocations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleHasPermission> RoleHasPermissions { get; set; }
        public DbSet<Testing> Testings { get; set; }
        public DbSet<TestingLocation> TestingLocations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<City> Cities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
            .HasMany(x => x.DailyCheckins)
            .WithOne(x => x.Account);

            builder.Entity<Account>()
            .HasMany(x => x.Itineraries)
            .WithOne(x => x.Account);

            builder.Entity<Account>()
            .HasMany(x => x.LocationCheckins)
            .WithOne(x => x.Account);

            builder.Entity<Account>()
            .HasMany(x => x.MedicalInfo)
            .WithOne(x => x.Account);
            
            builder.Entity<Account>()
            .HasMany(x => x.Testings)
            .WithOne(x => x.Account);

            builder.Entity<Account>()
            .HasOne(x => x.Profile)
            .WithOne(x => x.Account);

            builder.Entity<Account>()
            .HasMany(x => x.AccountHasRoles)
            .WithOne(x => x.Account);

            builder.Entity<Account>()
            .HasMany(x => x.AccountHasPermissions)
            .WithOne(x => x.Account);

            builder.Entity<Role>()
            .HasMany(x => x.AccountHasRoles)
            .WithOne(x => x.Role);

            builder.Entity<Role>()
            .HasMany(x => x.RoleHasPermissions)
            .WithOne(x => x.Role);

            builder.Entity<Permission>()
            .HasMany(x => x.RoleHasPermissions)
            .WithOne(x => x.Permission);

            builder.Entity<Permission>()
            .HasMany(x => x.AccountHasPermissions)
            .WithOne(x => x.Permission);

            builder.Entity<LocationCheckin>()
            .HasMany(x => x.PeopleLocations)
            .WithOne(x => x.LocationCheckin);

            builder.Entity<TestingLocation>()
            .HasMany(x => x.Testings)
            .WithOne(x => x.TestingLocation);

            builder.Entity<Testing>()
            .HasOne(a => a.TestingLocation)
            .WithMany(b => b.Testings);

            builder.Entity<City>()
            .HasMany(x => x.DepartureItineraries)
            .WithOne(x => x.DepartureCity)
            .HasForeignKey(x => x.DepartureCityId)
            .OnDelete(DeleteBehavior.ClientCascade);
        

            builder.Entity<City>()
            .HasMany(x => x.DestinationItineraties)
            .WithOne(x => x.DestinationCity)
            .HasForeignKey(x => x.DestinationCityId)
            .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<City>()
            .HasMany(x => x.TestingLocations)
            .WithOne(x => x.City);

        }
    }
}