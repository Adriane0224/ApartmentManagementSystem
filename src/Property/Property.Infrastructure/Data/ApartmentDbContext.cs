using Microsoft.EntityFrameworkCore;
using Property.Domain.Entities;
using Property.Infrastructure.ReadModels;

namespace Property.Infrastructure.Data
{
    public class ApartmentDbContext : DbContext
    {
        public ApartmentDbContext(DbContextOptions<ApartmentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Apartment");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApartmentDbContext).Assembly);

            // Ensure ApartmentUnit mapping is applied (value object conversion, etc.)
            // Your existing configuration class for ApartmentUnit will be picked up by ApplyConfigurationsFromAssembly.

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApartmentUnit> Apartments => Set<ApartmentUnit>();

        // NEW: join target for owner info (denormalized)
        public DbSet<UnitOwnerView> UnitOwners => Set<UnitOwnerView>();
    }
}
