using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ownership.Domain.Entities;

namespace Ownership.Domain.Repositories
{
    public interface IOwnerUnitRepository
    {
        Task<OwnerUnit?> GetByUnitAsync(Guid unitId, CancellationToken ct);
        Task AssignAsync(OwnerUnit link, CancellationToken ct);
        Task RemoveByUnitAsync(Guid unitId, CancellationToken ct);
    }
}

