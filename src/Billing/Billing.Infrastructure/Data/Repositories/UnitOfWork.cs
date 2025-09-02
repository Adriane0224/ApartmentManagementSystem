using Billing.Domain.Repositories;

namespace Billing.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BillingDbContext _db;

        public UnitOfWork(
            BillingDbContext db,
            IInvoiceRepository invoices,
            IPaymentRepository payments)
        {
            _db = db;
            Invoices = invoices;
            Payments = payments;
        }

        public IInvoiceRepository Invoices { get; }
        public IPaymentRepository Payments { get; }

        public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    }
}
