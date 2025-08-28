using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Response
{
    public class LeaseResponse
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid TenantId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public string Status { get; set; } = "Active";
    }
}
