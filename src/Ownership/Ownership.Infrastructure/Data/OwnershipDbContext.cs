using Microsoft.EntityFrameworkCore;
using Ownership.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Ownership.Infrastructure.Data
{
    public class OwnershipDbContext : DbContext
    {
        public OwnershipDbContext(DbContextOptions<OwnershipDbContext> options) : base(options) { }

        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<OwnerUnit> OwnerUnits => Set<OwnerUnit>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Ownership");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OwnershipDbContext).Assembly);
        }
    }
}
