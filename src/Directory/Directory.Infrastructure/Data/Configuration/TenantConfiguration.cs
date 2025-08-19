using Directory.Domain.Entities;
using Directory.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> tenant)
        {
            tenant.HasKey(u => u.Id);
            tenant.Property(u => u.Id)
                .HasConversion(u => u.Value, value => new TenantId(value));
            
        }
    }
}
