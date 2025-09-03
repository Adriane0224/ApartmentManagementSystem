using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Application.Response
{
    public sealed class ApartmentResponse
    {
        public Guid Id { get; set; }
        public string Unit { get; set; } = "";
        public int Floor { get; set; }
        public string Status { get; set; } = "";
        public string? Description { get; set; }

        public OwnerBrief? Owner { get; set; }
    }
}
