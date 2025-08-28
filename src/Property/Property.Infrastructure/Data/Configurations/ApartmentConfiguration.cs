using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Property.Domain.Entities;
using Property.Domain.ValueObject;

namespace Property.Infrastructure.Data.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<ApartmentUnit>
    {
        public void Configure(EntityTypeBuilder<ApartmentUnit> apartment)
        {
            apartment.HasKey(a => a.Id);
            apartment.Property(a => a.Id)
                .HasConversion(id => id.Value, value => new ApartmentId(value));
        }
    }
}
