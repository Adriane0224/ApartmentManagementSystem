using Leasing.Domain.Repositories;
using Leasing.Infrastructure.Data;

namespace Leasing.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeasingDbContext _context;
        private readonly ILeaseRepository _leaseRepository;
        public UnitOfWork(LeasingDbContext context, ILeaseRepository leaseRepository)
        {
            _context = context;
            _leaseRepository = leaseRepository;
        }
        public ILeaseRepository Leases => _leaseRepository;
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
