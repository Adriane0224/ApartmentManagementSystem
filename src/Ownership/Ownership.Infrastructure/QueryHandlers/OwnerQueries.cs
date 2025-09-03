using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ownership.Application.Queries;
using Ownership.Application.Response;
using Ownership.Infrastructure.Data;

namespace Ownership.Infrastructure.QueryHandlers
{
    public class OwnerQueries : IOwnerQueries
    {
        private readonly OwnershipDbContext _db;
        private readonly IMapper _mapper;

        public OwnerQueries(OwnershipDbContext db, IMapper mapper)
        { _db = db; _mapper = mapper; }

        public async Task<OwnerResponse?> GetOwnerAsync(Guid ownerId, CancellationToken ct)
        {
            var o = await _db.Owners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ownerId, ct);
            return o is null ? null : _mapper.Map<OwnerResponse>(o);
        }

        public async Task<OwnerResponse?> GetOwnerByUnitAsync(Guid unitId, CancellationToken ct)
        {
            var link = await _db.OwnerUnits.AsNoTracking().FirstOrDefaultAsync(x => x.UnitId == unitId, ct);
            if (link is null) return null;

            var o = await _db.Owners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == link.OwnerId, ct);
            return o is null ? null : _mapper.Map<OwnerResponse>(o);
        }
    }
}
