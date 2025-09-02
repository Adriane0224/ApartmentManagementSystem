using Billing.Application.Queries;
using Billing.Application.Response;
using Billing.Domain.Entities;
using Billing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.QueryHandlers
{
    public class InvoiceQueries : IInvoiceQueries
    {
        private readonly BillingDbContext _db;
        public InvoiceQueries(BillingDbContext db) => _db = db;

        public async Task<List<InvoiceResponse>> GetAsync(
            Guid? tenantId, Guid? leaseId, string? status, CancellationToken ct)
        {
            var q = _db.Invoices.AsNoTracking().AsQueryable();
            if (tenantId is { } t) q = q.Where(i => i.TenantId == t);
            if (leaseId is { } l) q = q.Where(i => i.LeaseId == l);
            if (!string.IsNullOrWhiteSpace(status) &&
                Enum.TryParse<RentInvoice.InvoiceStatus>(status, true, out var s))
                q = q.Where(i => i.Status == s);

            var list = await q.OrderByDescending(i => i.Year)
                              .ThenByDescending(i => i.Month)
                              .ToListAsync(ct);

            return list.Select(ToDto).ToList();
        }

        public async Task<List<InvoiceResponse>> GetOverdueAsync(DateOnly asOf, CancellationToken ct)
        {
            var list = await _db.Invoices.AsNoTracking()
                .Where(i => i.Status != RentInvoice.InvoiceStatus.Paid && i.DueDate < asOf)
                .ToListAsync(ct);

            foreach (var i in list) i.RecomputeStatus(asOf);
            return list.Select(ToDto).ToList();
        }

        private static InvoiceResponse ToDto(Billing.Domain.Entities.RentInvoice i) => new()
        {
            Id = i.Id.Value,
            LeaseId = i.LeaseId,
            ApartmentUnitId = i.ApartmentUnitId,
            TenantId = i.TenantId,
            UnitNumber = i.UnitNumber,   
            Floor = i.Floor,
            Year = i.Year,
            Month = i.Month,
            Amount = i.Amount,
            PaidTotal = i.PaidTotal,
            Status = i.Status.ToString(),
            DueDate = i.DueDate
        };
    }
}
