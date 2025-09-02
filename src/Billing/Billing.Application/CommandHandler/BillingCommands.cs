using Billing.Application.Commands;
using Billing.Application.Response;
using Billing.Domain.Entities;
using Billing.Domain.Ports;
using Billing.Domain.Repositories;
using Billing.Domain.ValueObject;
using FluentResults;
using MediatR;
using static Billing.Application.Commands.GenerateInvoicesCommands;
using static Billing.Application.Commands.RecordPaymentCommands;

namespace Billing.Application.CommandHandler
{
    public class BillingCommands :
        IRequestHandler<GenerateInvoicesForPeriodCommand, Result<List<InvoiceResponse>>>,
        IRequestHandler<RecordPaymentCommand, Result<PaymentResponse>>
    {
        private readonly ILeasingReadPort _leasing;
        private readonly IApartmentsReadPort _apartments; // NEW
        private readonly IInvoiceRepository _invoices;
        private readonly IPaymentRepository _payments;
        private readonly IUnitOfWork _uow;

        public BillingCommands(
            ILeasingReadPort leasing,
            IApartmentsReadPort apartments,      // NEW
            IInvoiceRepository invoices,
            IPaymentRepository payments,
            IUnitOfWork uow)
        {
            _leasing = leasing;
            _apartments = apartments;            // NEW
            _invoices = invoices;
            _payments = payments;
            _uow = uow;
        }

        public async Task<Result<List<InvoiceResponse>>> Handle(GenerateInvoicesForPeriodCommand r, CancellationToken ct)
        {
            if (!TryParsePeriod(r.Period, out var year, out var month))
                return Result.Fail("Invalid period. Use YYYY-MM.");

            var (from, to) = MonthRange(year, month);
            var dueDate = new DateOnly(year, month, Math.Min(5, DateTime.DaysInMonth(year, month)));

            var leases = await _leasing.GetActiveLeasesAsync(ct);
            var targetLeases = leases.Where(l => l.StartDate <= to && l.EndDate >= from).ToList();

            // avoid refetching the same unit repeatedly
            var unitCache = new Dictionary<Guid, (string? unitNumber, int? floor)>();

            var created = new List<InvoiceResponse>();
            foreach (var l in targetLeases)
            {
                var exists = await _invoices.GetByLeasePeriodAsync(l.Id, year, month, ct);
                if (exists is not null) continue;

                // fetch unit snapshot (UnitNumber/Floor)
                if (!unitCache.TryGetValue(l.ApartmentId, out var snap))
                {
                    var unit = await _apartments.GetUnitAsync(l.ApartmentId, ct); // adjust if your lease has ApartmentUnitId
                    snap = (unit?.UnitNumber, unit?.Floor);
                    unitCache[l.ApartmentId] = snap;
                }

                // NOTE: ensure RentInvoice.Create supports unit snapshot parameters
                // signature: Create(leaseId, apartmentUnitId, tenantId, year, month, amount, dueDate, unitNumber, floor)
                var inv = RentInvoice.Create(
                    l.Id,
                    l.ApartmentId,
                    l.TenantId,
                    year,
                    month,
                    l.MonthlyRent,
                    dueDate,
                    snap.unitNumber,
                    snap.floor);

                await _invoices.AddAsync(inv, ct);
                created.Add(ToDto(inv));
            }

            await _uow.SaveChangesAsync(ct);
            return Result.Ok(created);
        }

        public async Task<Result<PaymentResponse>> Handle(RecordPaymentCommand r, CancellationToken ct)
        {
            var invoice = await _invoices.GetByIdAsync(new InvoiceId(r.InvoiceId), ct);
            if (invoice is null) return Result.Fail("Invoice not found.");

            var payerId = r.PayerId ?? invoice.TenantId;

            var payment = Payment.Create(r.InvoiceId, payerId, r.Amount, DateTimeOffset.UtcNow, r.Method, r.Reference);
            invoice.ApplyPayment(r.Amount);

            await _payments.AddAsync(payment, ct);
            await _invoices.UpdateAsync(invoice, ct);
            await _uow.SaveChangesAsync(ct);

            return Result.Ok(new PaymentResponse
            {
                Id = payment.Id,
                InvoiceId = payment.InvoiceId,
                PayerId = payment.PayerId,
                Amount = payment.Amount,
                Method = payment.Method,
                Reference = payment.Reference,
                ReceivedAt = payment.ReceivedAt
            });
        }

        private static bool TryParsePeriod(string p, out int year, out int month)
        {
            year = 0; month = 0;
            if (string.IsNullOrWhiteSpace(p)) return false;
            var parts = p.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;
            return int.TryParse(parts[0], out year) && int.TryParse(parts[1], out month) &&
                   year >= 2000 && month is >= 1 and <= 12;
        }

        private static (DateOnly from, DateOnly to) MonthRange(int year, int month)
        {
            var start = new DateOnly(year, month, 1);
            var end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));
            return (start, end);
        }

        private static InvoiceResponse ToDto(RentInvoice i) => new()
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
