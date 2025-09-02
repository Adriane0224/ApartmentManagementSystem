using Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Billing.Infrastructure.Data
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        public DbSet<RentInvoice> Invoices => Set<RentInvoice>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Billing");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
