using Directory.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory.Application.Queries
{
    public interface ITenantQueries
    {
        Task<List<TenantResponse>> GetAllAsync();
        Task<TenantResponse?> GetTenantByIdAsync(Guid id);
    }
}
