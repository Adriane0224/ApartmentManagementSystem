using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Domain.Ports
{
    public interface IApartmentsReadPort
    {
        Task<UnitDto?> GetUnitAsync(Guid unitId, CancellationToken ct);
    }

    public class UnitDto
    {
        public Guid Id { get; set; }
        public string? UnitNumber { get; set; }
        public int? Floor { get; set; }
    }
}
