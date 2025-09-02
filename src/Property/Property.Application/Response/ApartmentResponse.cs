using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Application.Response
{
    public class ApartmentResponse
    {

        public Guid Id { get; set; }
        public string Unit { get; set; } = null!;
        public int Floor { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
        public string? Owner { get; set; }


    }
}
