using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        // Filled by queries; omitted when null so existing clients don’t break
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TenantResponse? Tenant { get; set; }
    }
}
