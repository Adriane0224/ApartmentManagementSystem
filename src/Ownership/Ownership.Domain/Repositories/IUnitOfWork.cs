using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IOwnerRepository Owners { get; }
        IOwnerUnitRepository OwnerUnits { get; }
        Task SaveChangesAsync(CancellationToken ct);
    }
}

