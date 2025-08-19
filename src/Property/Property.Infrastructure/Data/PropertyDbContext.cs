using Microsoft.EntityFrameworkCore;
using Property.Domain.Entities;

namespace Property.Infrastructure.Data;

public class PropertyDbContext : DbContext
{
    public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options) { }
    public DbSet<Building> Buildings => Set<Building>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyDbContext).Assembly);
}
