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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApartmentUnit> Apartments => Set<ApartmentUnit>();
        public DbSet<UnitOwnerView> UnitOwners => Set<UnitOwnerView>();
    }
}
