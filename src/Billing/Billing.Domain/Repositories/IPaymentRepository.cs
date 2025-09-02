using Billing.Domain.Entities;

namespace Billing.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment, CancellationToken ct);
        Task<List<Payment>> GetByInvoiceAsync(Guid invoiceId, CancellationToken ct);
        Task<List<Payment>> GetByPayerAsync(Guid payerId, CancellationToken ct);
    }
}
