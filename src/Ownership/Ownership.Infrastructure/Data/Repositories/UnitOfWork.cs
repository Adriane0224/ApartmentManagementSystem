using Ownership.Domain.Repositories;

namespace Ownership.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OwnershipDbContext _db;
        public IOwnerRepository Owners { get; }
        public IOwnerUnitRepository OwnerUnits { get; }

        public UnitOfWork(OwnershipDbContext db, IOwnerRepository owners, IOwnerUnitRepository ownerUnits)
        { _db = db; Owners = owners; OwnerUnits = ownerUnits; }

        public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    }
}
