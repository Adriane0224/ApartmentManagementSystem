using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billing.Application.Response;

namespace Billing.Application.Queries
{
    public interface IInvoiceQueries
    {
        Task<List<InvoiceResponse>> GetAsync(Guid? tenantId, Guid? leaseId, string? status, CancellationToken ct);
        Task<List<InvoiceResponse>> GetOverdueAsync(DateOnly asOf, CancellationToken ct);
    }
}
