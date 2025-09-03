using Microsoft.EntityFrameworkCore;
using Property.Application.Queries;
using Property.Application.Response;
using Property.Domain.ValueObject;
using Property.Infrastructure.Data;
using static Property.Domain.Entities.ApartmentUnit;

namespace Property.Infrastructure.QueryHandlers
{
    public class ApartmentQueries : IApartmentQueries
    {
        private readonly ApartmentDbContext _context;

        public ApartmentQueries(ApartmentDbContext context)
        {
            _context = context;
        }
        private async Task<List<ApartmentResponse>> ProjectWithOwnerAsync(
            IQueryable<Property.Domain.Entities.ApartmentUnit> source,
            CancellationToken ct = default)
        {
            var aps = await source
                .AsNoTracking()
                .Select(a => new
                {
                    Id = a.Id.Value, 
                    a.Unit,
                    a.Floor,
                    a.Status,
                    a.Description
                })
                .ToListAsync(ct);

            if (aps.Count == 0) return new List<ApartmentResponse>();

            var unitIds = aps.Select(x => x.Id).Distinct().ToList();

            var owners = await _context.UnitOwners
                .AsNoTracking()
                .Where(u => unitIds.Contains(u.UnitId))
                .ToListAsync(ct);

            var ownerMap = owners.ToDictionary(o => o.UnitId, o => o);
            var result = new List<ApartmentResponse>(aps.Count);
            foreach (var x in aps)
            {
                ownerMap.TryGetValue(x.Id, out var o);

                result.Add(new ApartmentResponse
                {
                    Id = x.Id,
                    Unit = x.Unit,
                    Floor = x.Floor,
                    Status = x.Status.ToString(),
                    Description = x.Description,
                    Owner = o == null ? null : new OwnerBrief
                    {
                        Id = o.OwnerId,
                        Name = o.Name,
                        Email = o.Email,
                        Phone = o.Phone
                    }
                });
            }
            return result;
        }
        public async Task<List<ApartmentResponse>> GetAllOccupiedApartmentsAsync()
        {
            var q = _context.Apartments.Where(a => a.Status == UnitStatus.Occupied);
            return await ProjectWithOwnerAsync(q);
        }

        public async Task<List<ApartmentResponse>> GetAllApartmentsAsync()
        {
            return await ProjectWithOwnerAsync(_context.Apartments);
        }

        public async Task<List<ApartmentResponse>> GetAllUnderMaintenanceApartments()
        {
            var q = _context.Apartments.Where(a => a.Status == UnitStatus.Maintenance);
            return await ProjectWithOwnerAsync(q);
        }

        public async Task<List<ApartmentResponse>> GetAllVacantApartmentsAsync()
        {
            var q = _context.Apartments.Where(a => a.Status == UnitStatus.Available);
            return await ProjectWithOwnerAsync(q);
        }

        public async Task<ApartmentResponse?> GetApartmentByIdAsync(string? unit)
        {
            if (string.IsNullOrWhiteSpace(unit)) return null;

            var q = _context.Apartments.Where(a => a.Unit == unit);
            var list = await ProjectWithOwnerAsync(q);
            return list.FirstOrDefault();
        }

        public async Task<List<ApartmentResponse>> GetAllAvailableApartments()
        {
            var q = _context.Apartments.Where(a => a.Status == UnitStatus.Available);
            return await ProjectWithOwnerAsync(q);
        }

        public async Task<ApartmentResponse?> GetApartmentByIdAsync(Guid id)
        {
            var q = _context.Apartments.Where(a => a.Id == new ApartmentId(id));
            var list = await ProjectWithOwnerAsync(q);
            return list.FirstOrDefault();
        }
    }
}
