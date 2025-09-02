using Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billing.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Amount).HasColumnType("decimal(18,2)");

            b.HasIndex(x => x.InvoiceId);
            b.HasIndex(x => x.PayerId);

            b.Property(x => x.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
