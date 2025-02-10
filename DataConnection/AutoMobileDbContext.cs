using AutoMobile.Models.Masters;
using AutoMobile.Models.model;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileERP.DataConnection
{
    public class AutoMobileDbContext : DbContext
    {
        public AutoMobileDbContext(DbContextOptions<AutoMobileDbContext> options) : base(options)
        {

        }
        public DbSet<CompanyRegistration> CompanyRegistration { get; set; }
        public DbSet<Bike_Brand> Bike_Brand { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Bike> Bike { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike_Brand>().ToTable("Bike_Brand"); // Ensure this matches your database
            modelBuilder.Entity<Bike>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");
        }

    }
}
