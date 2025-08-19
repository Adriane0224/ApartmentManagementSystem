using Directory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory.Domain.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(Guid id);
        Task<Tenant?> GetByEmailAsync(string email);
        Task AddAsync(Tenant tenant);
        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
