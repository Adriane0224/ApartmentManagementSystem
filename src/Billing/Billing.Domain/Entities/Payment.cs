using ApartmentManagement.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Domain.Entities
{
    public class Payment : Entity
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Guid PayerId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTimeOffset ReceivedAt { get; private set; }
        public string Method { get; private set; } = "Manual";
        public string? Reference { get; private set; }
        public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

        protected Payment() { }

        public static Payment Create(Guid invoiceId, Guid payerId, decimal amount,
            DateTimeOffset receivedAt, string method, string? reference)
        {
            if (amount <= 0) throw new ArgumentException("Payment amount must be positive.");
            if (payerId == Guid.Empty) throw new ArgumentException("PayerId is required.", nameof(payerId));

            return new Payment
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoiceId,
                PayerId = payerId,
                Amount = amount,
                ReceivedAt = receivedAt,
                Method = string.IsNullOrWhiteSpace(method) ? "Manual" : method,
                Reference = reference
            };
        }
    }
}
