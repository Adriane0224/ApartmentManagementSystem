using Ownership.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task AddAsync(Owner owner, CancellationToken ct);
        Task<Owner?> GetByIdAsync(Guid id, CancellationToken ct);
        Task UpdateAsync(Owner owner, CancellationToken ct);
    }
}
