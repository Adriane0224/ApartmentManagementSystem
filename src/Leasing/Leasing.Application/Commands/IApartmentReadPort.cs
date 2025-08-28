using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Commands
{
    public interface IApartmentReadPort
    {
        Task<bool> IsAvailableAsync(Guid apartmentId, DateOnly start, DateOnly end, CancellationToken ct);
    }
}
