using ApartmentManagement.SharedKernel.Entities;
using Billing.Domain.ValueObject;

namespace Billing.Domain.Entities
{
    public class RentInvoice : Entity
    {
        public InvoiceId Id { get; private set; }
        public Guid LeaseId { get; private set; }
        public Guid ApartmentUnitId { get; private set; }
        public Guid TenantId { get; private set; }

        // apartment unit 
        public string? UnitNumber { get; private set; }
        public int? Floor { get; private set; }

        public int Year { get; private set; }
        public int Month { get; private set; }

        public decimal Amount { get; private set; }
        public decimal PaidTotal { get; private set; }
        public DateOnly DueDate { get; private set; }
        public InvoiceStatus Status { get; private set; }

        public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

        public enum InvoiceStatus { Pending = 1, Paid = 2, Overdue = 3, Cancelled = 4 }

        protected RentInvoice() { }

        public static RentInvoice Create(
            Guid leaseId,
            Guid apartmentUnitId,
            Guid tenantId,
            int year,
            int month,
            decimal amount,
            DateOnly dueDate,
            string? unitNumber = null,
            int? floor = null)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.", nameof(amount));
            if (year < 2000 || month is < 1 or > 12) throw new ArgumentException("Invalid period.");

            return new RentInvoice
            {
                Id = new InvoiceId(Guid.NewGuid()),
                LeaseId = leaseId,
                ApartmentUnitId = apartmentUnitId,
                TenantId = tenantId,
                UnitNumber = unitNumber,
                Floor = floor,
                Year = year,
                Month = month,
                Amount = amount,
                PaidTotal = 0m,
                DueDate = dueDate,
                Status = InvoiceStatus.Pending
            };
        }

        public void ApplyPayment(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Payment must be positive.");
            if (Status is InvoiceStatus.Cancelled) throw new InvalidOperationException("Invoice cancelled.");
            PaidTotal += amount;
            if (PaidTotal >= Amount) Status = InvoiceStatus.Paid;
        }

        public void RecomputeStatus(DateOnly asOf)
        {
            if (Status is InvoiceStatus.Paid or InvoiceStatus.Cancelled) return;
            Status = asOf > DueDate ? InvoiceStatus.Overdue : InvoiceStatus.Pending;
        }
    }
}
