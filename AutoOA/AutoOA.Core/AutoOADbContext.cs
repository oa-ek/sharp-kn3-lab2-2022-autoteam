using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoOA.Core
{
    public class AutoOADbContext : IdentityDbContext<User>
    {
        public AutoOADbContext(DbContextOptions<AutoOADbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SalesData> SalesData { get; set; }
        public DbSet<GearBox> GearBoxes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<DriveType> DriveTypes { get; set; }
        public DbSet<VehicleColor> VehicleColors { get; set; }
    }
}