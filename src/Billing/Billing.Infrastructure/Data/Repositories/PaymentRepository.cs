using Billing.Domain.Entities;
using Billing.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BillingDbContext _db;
        public PaymentRepository(BillingDbContext db) => _db = db;

        public async Task AddAsync(Payment payment, CancellationToken ct)
            => await _db.Payments.AddAsync(payment, ct);

        public Task<List<Payment>> GetByInvoiceAsync(Guid invoiceId, CancellationToken ct)
            => _db.Payments.Where(p => p.InvoiceId == invoiceId)
                           .OrderByDescending(p => p.ReceivedAt)
                           .ToListAsync(ct);

        public Task<List<Payment>> GetByPayerAsync(Guid payerId, CancellationToken ct)
            => _db.Payments.Where(p => p.PayerId == payerId)
                           .OrderByDescending(p => p.ReceivedAt)
                           .ToListAsync(ct);
    }
}
