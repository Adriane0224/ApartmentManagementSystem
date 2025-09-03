using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Property.Infrastructure.ReadModels;

namespace Property.Infrastructure.Data.Configurations
{
    public class UnitOwnerViewConfiguration : IEntityTypeConfiguration<UnitOwnerView>
    {
        public void Configure(EntityTypeBuilder<UnitOwnerView> b)
        {
            b.ToTable("UnitOwnerView", "Apartment");

            b.HasKey(x => x.UnitId);
            b.Property(x => x.UnitId).IsRequired();
            b.Property(x => x.OwnerId).IsRequired();

            b.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            b.Property(x => x.Email).HasMaxLength(200);
            b.Property(x => x.Phone).HasMaxLength(50);
        }
    }
}
