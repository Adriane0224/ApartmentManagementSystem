using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Response
{
    public class TenantResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? Phone { get; init; }
    }
}
