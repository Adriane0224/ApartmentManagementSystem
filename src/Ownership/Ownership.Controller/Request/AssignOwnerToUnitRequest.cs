using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Controller.Request
{
    public class AssignOwnerToUnitRequest
    {
        public Guid OwnerId { get; set; }
        public Guid UnitId { get; set; }
    }
}

