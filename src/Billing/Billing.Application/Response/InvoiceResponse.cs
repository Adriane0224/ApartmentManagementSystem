using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Application.Response
{
    public  class InvoiceResponse
    {
        public Guid Id { get; set; }
        public Guid LeaseId { get; set; }
        public Guid ApartmentUnitId { get; set; }
        public Guid TenantId { get; set; }
        public string? UnitNumber { get; set; }
        public int? Floor { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidTotal { get; set; }
        public string Status { get; set; } = "Pending";
        public DateOnly DueDate { get; set; }

        public InvoiceResponse() { }
    }
}
