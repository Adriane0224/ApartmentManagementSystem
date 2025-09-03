using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Application.Response
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid PayerId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ReceivedAt { get; set; }
        public string Method { get; set; } = "Manual";
        public string? Reference { get; set; }
    }
}

