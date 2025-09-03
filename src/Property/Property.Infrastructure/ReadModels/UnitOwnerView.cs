using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Infrastructure.ReadModels
{
    public sealed class UnitOwnerView
    {
        public Guid UnitId { get; set; } 
        public Guid OwnerId { get; set; }

        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}



