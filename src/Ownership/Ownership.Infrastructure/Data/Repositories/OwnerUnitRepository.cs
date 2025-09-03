using Microsoft.EntityFrameworkCore;
using Ownership.Domain.Entities;
using Ownership.Domain.Repositories;

namespace Ownership.Infrastructure.Data.Repositories
{
    public class OwnerUnitRepository : IOwnerUnitRepository
    {
        private readonly OwnershipDbContext _db;
        public OwnerUnitRepository(OwnershipDbContext db) => _db = db;

        public Task<OwnerUnit?> GetByUnitAsync(Guid unitId, CancellationToken ct)
            => _db.OwnerUnits.FirstOrDefaultAsync(l => l.UnitId == unitId, ct);

        public Task AssignAsync(OwnerUnit link, CancellationToken ct)
            => _db.OwnerUnits.AddAsync(link, ct).AsTask();

        public async Task RemoveByUnitAsync(Guid unitId, CancellationToken ct)
        {
            var link = await _db.OwnerUnits.FirstOrDefaultAsync(l => l.UnitId == unitId, ct);
            if (link != null) _db.OwnerUnits.Remove(link);
        }
    }
}
