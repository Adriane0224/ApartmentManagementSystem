using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Controllers.Request
{
    public class RenewLeaseRequest
    {
        public DateOnly NewEndDate { get; set; }
        public decimal? NewMonthlyRent { get; set; }
    }
}
