using Leasing.Domain.Entities;
using Leasing.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Domain.Repositories
{
    public interface ILeaseRepository
    {
        Task AddAsync(Lease lease, CancellationToken cancellationToken);
        Task<Lease?> GetByIdAsync(LeaseId id, CancellationToken cancellationToken);
        Task<Lease?> GetByIdForUpdateAsync(LeaseId id, CancellationToken cancellationToken);
        Task<bool> ExistsOverlapAsync(Guid apartmentId, DateOnly start, DateOnly end, CancellationToken cancellationToken);

    }
}
