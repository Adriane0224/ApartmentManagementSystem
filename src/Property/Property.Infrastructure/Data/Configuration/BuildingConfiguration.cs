using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Property.Domain.Entities;
using Property.Domain.ValueObject;

namespace Property.Infrastructure.Configurations;

public sealed class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> b)
    {
        b.ToTable("Buildings", "Property");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id)
         .HasConversion(id => id.Value, v => new BuildingId(v))
         .ValueGeneratedNever();

        b.Property(x => x.Name).IsRequired().HasMaxLength(200);
    }
}
