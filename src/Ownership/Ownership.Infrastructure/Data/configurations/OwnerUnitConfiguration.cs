using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ownership.Domain.Entities;

namespace Ownership.Infrastructure.Data.Configurations
{
    public class OwnerUnitConfiguration : IEntityTypeConfiguration<OwnerUnit>
    {
        public void Configure(EntityTypeBuilder<OwnerUnit> b)
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.UnitId).IsUnique();
            b.Property(x => x.OwnerId).IsRequired();
            b.Property(x => x.UnitId).IsRequired();
        }
    }
}
