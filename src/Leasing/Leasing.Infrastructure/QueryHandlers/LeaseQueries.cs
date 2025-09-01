using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Leasing.Application.Queries;
using Leasing.Application.Response;
using Leasing.Domain.Entities;
using Leasing.Domain.ValueObject;
using Leasing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Leasing.Infrastructure.QueryHandlers
{
    file class TenantWireDto
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }

    public class LeaseQueries : ILeaseQueries
    {
        private readonly LeasingDbContext _db;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _http;

        public LeaseQueries(LeasingDbContext db, IMapper mapper, IHttpClientFactory http)
        {
            _db = db;
            _mapper = mapper;
            _http = http;
        }

        public async Task<LeaseResponse?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var dto = await _db.Leases.AsNoTracking()
                .Where(l => l.Id == new LeaseId(id))
                .ProjectTo<LeaseResponse>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(ct);

            if (dto is null) return null;

            dto.Tenant = await FetchTenantAsync(dto.TenantId, ct);
            return dto;
        }

        public async Task<List<LeaseResponse>> GetActiveByApartmentAsync(Guid apartmentId, CancellationToken ct)
        {
            var list = await _db.Leases.AsNoTracking()
                .Where(l => l.ApartmentId == apartmentId && l.Status == Lease.LeaseStatus.Active)
                .ProjectTo<LeaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            await EnrichTenantsAsync(list, ct);
            return list;
        }

        public async Task<List<LeaseResponse>> GetActiveAsync(CancellationToken ct)
        {
            var list = await _db.Leases.AsNoTracking()
                .Where(l => l.Status == Lease.LeaseStatus.Active)
                .ProjectTo<LeaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            await EnrichTenantsAsync(list, ct);
            return list;
        }

        private async Task EnrichTenantsAsync(List<LeaseResponse> leases, CancellationToken ct)
        {
            if (leases.Count == 0) return;

            var ids = leases.Select(x => x.TenantId).Distinct().ToList();
            var map = await FetchTenantMapAsync(ids, ct);

            foreach (var l in leases)
                if (map.TryGetValue(l.TenantId, out var t))
                    l.Tenant = t;
        }

        private async Task<TenantResponse?> FetchTenantAsync(Guid tenantId, CancellationToken ct)
        {
            var client = _http.CreateClient("TenantApi");
            try
            {
                // directory route to get the details of tenant
                var t = await client.GetFromJsonAsync<TenantWireDto>($"/api/tenant/getById/{tenantId}", ct);
                return t is null ? null : new TenantResponse { Id = t.Id, Name = t.Name, Email = t.Email, Phone = t.Phone };
            }
            catch
            {
                return null;
            }
        }

        private async Task<Dictionary<Guid, TenantResponse>> FetchTenantMapAsync(IEnumerable<Guid> ids, CancellationToken ct)
        {
            var client = _http.CreateClient("TenantApi");

            var sem = new SemaphoreSlim(8);
            var dict = new ConcurrentDictionary<Guid, TenantResponse>();

            var tasks = ids.Select(async id =>
            {
                await sem.WaitAsync(ct);
                try
                {
                    var t = await client.GetFromJsonAsync<TenantWireDto>($"/api/tenant/getById/{id}", ct);
                    if (t is not null)
                        dict[id] = new TenantResponse { Id = t.Id, Name = t.Name, Email = t.Email, Phone = t.Phone };
                }
                catch { /* ignore and continue */ }
                finally { sem.Release(); }
            });

            await Task.WhenAll(tasks);
            return dict.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
