using Billing.Domain.Entities;
using Billing.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Infrastructure.Data.Configurations
{
    public sealed class RentInvoiceConfiguration : IEntityTypeConfiguration<RentInvoice>
    {
        public void Configure(EntityTypeBuilder<RentInvoice> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id)
                .HasConversion(v => v.Value, v => new InvoiceId(v))
                .ValueGeneratedNever();

            b.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            b.Property(x => x.PaidTotal).HasColumnType("decimal(18,2)");

            // NEW
            b.Property(x => x.UnitNumber).HasMaxLength(50);
            b.Property(x => x.Floor);

            b.HasIndex(x => new { x.LeaseId, x.Year, x.Month }).IsUnique();

            b.Property(x => x.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
