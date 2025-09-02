using System.Net.Http.Json;
using Billing.Domain.Ports;

namespace Billing.Infrastructure.ReadClients
{
    public sealed class LeasingReadClient : ILeasingReadPort
    {
        private readonly HttpClient _http;
        public LeasingReadClient(HttpClient http) => _http = http;

        private class LeaseApiRow
        {
            public Guid id { get; set; }
            public Guid apartmentId { get; set; }
            public Guid tenantId { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public decimal monthlyRent { get; set; }
            public string status { get; set; } = "";
        }

        public async Task<List<LeaseDto>> GetActiveLeasesAsync(CancellationToken ct)
        {
            var rows = await _http.GetFromJsonAsync<List<LeaseApiRow>>("/api/leases/getAll", ct)
                       ?? new List<LeaseApiRow>();

            return rows.Select(x => new LeaseDto
            {
                Id = x.id,
                ApartmentId = x.apartmentId,
                TenantId = x.tenantId,
                StartDate = DateOnly.FromDateTime(x.startDate),
                EndDate = DateOnly.FromDateTime(x.endDate),
                MonthlyRent = x.monthlyRent,
                Status = x.status
            }).ToList();
        }
    }
}
