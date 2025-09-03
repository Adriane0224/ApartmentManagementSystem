using Microsoft.EntityFrameworkCore;
using Ownership.Domain.Entities;
using Ownership.Domain.Repositories;

namespace Ownership.Infrastructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly OwnershipDbContext _db;
        public OwnerRepository(OwnershipDbContext db) => _db = db;

        public Task<Owner?> GetByIdAsync(Guid id, CancellationToken ct)
            => _db.Owners.FirstOrDefaultAsync(o => o.Id == id, ct);

        public Task AddAsync(Owner owner, CancellationToken ct)
            => _db.Owners.AddAsync(owner, ct).AsTask();

        public Task UpdateAsync(Owner owner, CancellationToken ct)
        { _db.Owners.Update(owner); return Task.CompletedTask; }
    }
}
