using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Application.Response
{
    public class OwnerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}

