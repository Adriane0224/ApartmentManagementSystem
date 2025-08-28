using Leasing.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Queries
{
    public interface ILeaseQueries
    {
        Task<LeaseResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<LeaseResponse>> GetActiveByApartmentAsync(Guid apartmentId, CancellationToken cancellationToken);
        Task<List<LeaseResponse>> GetActiveAsync(CancellationToken cancellationToken);
    }
}
