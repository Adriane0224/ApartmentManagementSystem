using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Commands
{
    public interface ITenantReadPort
    {
        Task<bool> ExistsAsync(Guid tenantId, CancellationToken ct);
    }
}
