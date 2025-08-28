using Leasing.Domain.Entities;
using Leasing.Domain.Repositories;
using Leasing.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Leasing.Infrastructure.Data.Repositories
{
    public class LeaseRepository : ILeaseRepository
    {
        private readonly LeasingDbContext _context;

        public LeaseRepository(LeasingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Lease lease, CancellationToken cancellationToken)
        {
            await _context.Leases.AddAsync(lease, cancellationToken);
        }

        public Task DeleteAsync(Lease lease)
        {
            _context.Leases.Remove(lease);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsOverlapAsync(Guid apartmentId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
        {
            return _context.Leases
                .AnyAsync(l => l.ApartmentId == apartmentId &&
                               l.StartDate < end &&
                               l.EndDate > start, cancellationToken);
        }

        public async Task<List<Lease>> GetAllAsync()
        {
            return await _context.Leases.ToListAsync();
        }

        public Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken)
        {
            return _context.Leases
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public Task<Lease?> GetByIdForUpdateAsync(LeaseId id, CancellationToken cancellationToken)
        {
            return _context.Leases
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Lease lease)
        {
            _context.Leases.Update(lease);
            return Task.CompletedTask;
        }
    }
}
