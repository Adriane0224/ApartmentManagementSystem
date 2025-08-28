using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IApartmentRepository Apartments { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
