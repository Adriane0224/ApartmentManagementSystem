using Billing.Domain.Entities;
using Billing.Domain.ValueObject;

namespace Billing.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<RentInvoice?> GetByIdAsync(InvoiceId id, CancellationToken ct);
        Task<RentInvoice?> GetByLeasePeriodAsync(Guid leaseId, int year, int month, CancellationToken ct);
        Task AddAsync(RentInvoice invoice, CancellationToken ct);
        Task UpdateAsync(RentInvoice invoice, CancellationToken ct);
        Task<List<RentInvoice>> GetByFilterAsync(Guid? tenantId, Guid? leaseId, string? status, CancellationToken ct);
        Task<List<RentInvoice>> GetOverdueAsync(DateOnly asOf, CancellationToken ct);
    }
}
