using Billing.Domain.Entities;
using Billing.Domain.Repositories;
using Billing.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly BillingDbContext _db;
        public InvoiceRepository(BillingDbContext db) => _db = db;

        public Task<RentInvoice?> GetByIdAsync(InvoiceId id, CancellationToken ct)
             => _db.Invoices.FirstOrDefaultAsync(i => i.Id.Equals(id), ct);


        public Task<RentInvoice?> GetByLeasePeriodAsync(Guid leaseId, int year, int month, CancellationToken ct)
            => _db.Invoices.FirstOrDefaultAsync(i => i.LeaseId == leaseId && i.Year == year && i.Month == month, ct);

        public async Task AddAsync(RentInvoice invoice, CancellationToken ct)
            => await _db.Invoices.AddAsync(invoice, ct);

        public Task UpdateAsync(RentInvoice invoice, CancellationToken ct)
        { _db.Invoices.Update(invoice); return Task.CompletedTask; }

        public async Task<List<RentInvoice>> GetByFilterAsync(Guid? tenantId, Guid? leaseId, string? status, CancellationToken ct)
        {
            var q = _db.Invoices.AsQueryable();
            if (tenantId is { } t) q = q.Where(i => i.TenantId == t);
            if (leaseId is { } l) q = q.Where(i => i.LeaseId == l);
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<RentInvoice.InvoiceStatus>(status, true, out var s))
                q = q.Where(i => i.Status == s);

            return await q.OrderByDescending(i => i.Year).ThenByDescending(i => i.Month).ToListAsync(ct);
        }

        public async Task<List<RentInvoice>> GetOverdueAsync(DateOnly asOf, CancellationToken ct)
        {
            var list = await _db.Invoices
                .Where(i => i.Status != RentInvoice.InvoiceStatus.Paid && i.DueDate < asOf)
                .ToListAsync(ct);

            foreach (var i in list) i.RecomputeStatus(asOf);
            return list;
        }
    }
}
