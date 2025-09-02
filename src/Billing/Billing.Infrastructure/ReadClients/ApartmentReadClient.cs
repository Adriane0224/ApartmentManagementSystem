using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Billing.Domain.Ports;

namespace Billing.Infrastructure.ReadClients
{
    public class ApartmentsReadClient : IApartmentsReadPort
    {
        private readonly HttpClient _http;
        public ApartmentsReadClient(HttpClient http) => _http = http;
        private class UnitApiRow
        {
            public Guid id { get; set; }

            [JsonPropertyName("unit")]
            public string? unit_from_unit { get; set; }

            public string? unitNumber { get; set; }
            [JsonPropertyName("number")]
            public string? unit_from_number { get; set; }
            [JsonPropertyName("unitNo")]
            public string? unit_from_unitNo { get; set; }

            public int? floor { get; set; }
        }

        public async Task<UnitDto?> GetUnitAsync(Guid unitId, CancellationToken ct)
        {
            var candidates = new[]
            {
                $"/api/property/{unitId}",
                $"/api/apartments/getById/{unitId}",
                $"/api/apartments/{unitId}",
                $"/api/units/{unitId}",
                $"/api/apartmentUnits/{unitId}"
            };

            foreach (var url in candidates)
            {
                using var resp = await _http.GetAsync(url, ct);
                if (resp.StatusCode == HttpStatusCode.NotFound) continue;
                if (!resp.IsSuccessStatusCode) continue;

                var row = await resp.Content.ReadFromJsonAsync<UnitApiRow>(cancellationToken: ct);
                if (row is null) return null;
                var unitNumber =
                    row.unitNumber ??
                    row.unit_from_unit ??
                    row.unit_from_number ??
                    row.unit_from_unitNo;

                return new UnitDto
                {
                    Id = row.id == Guid.Empty ? unitId : row.id,
                    UnitNumber = unitNumber,
                    Floor = row.floor
                };
            }

            return null;
        }
    }
}
