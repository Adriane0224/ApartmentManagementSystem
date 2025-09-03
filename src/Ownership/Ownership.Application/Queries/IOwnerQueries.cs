using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ownership.Application.Response;

namespace Ownership.Application.Queries
{
    public interface IOwnerQueries
    {
        Task<OwnerResponse?> GetOwnerAsync(Guid ownerId, CancellationToken ct);
        Task<OwnerResponse?> GetOwnerByUnitAsync(Guid unitId, CancellationToken ct);
    }
}

