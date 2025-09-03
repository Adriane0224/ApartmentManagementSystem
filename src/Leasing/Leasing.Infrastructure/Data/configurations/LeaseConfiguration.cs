using Leasing.Domain.Entities;
using Leasing.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leasing.Infrastructure.Data.Configurations
{
    public class LeaseConfiguration : IEntityTypeConfiguration<Lease>
    {
        public void Configure(EntityTypeBuilder<Lease> b)
        {
            b.HasKey(l => l.Id);
            b.Property(l => l.Id).HasConversion(id => id.Value, v => new LeaseId(v)).ValueGeneratedNever();

            b.Property(l => l.ApartmentId).IsRequired();
            b.Property(l => l.TenantId).IsRequired();
            b.Property(l => l.StartDate).HasColumnType("date");
            b.Property(l => l.EndDate).HasColumnType("date");
            b.Property(l => l.MonthlyRent).HasColumnType("decimal(18,2)");
            b.Property(l => l.SecurityDeposit).HasColumnType("decimal(18,2)");
            b.Property(l => l.Status).HasConversion<int>().IsRequired();
            //b.Property(l => l.MonthlyRent).HasColumnType("decimal(18,2)");
            b.HasIndex(l => new { l.ApartmentId, l.Status });
        }
    }
}
