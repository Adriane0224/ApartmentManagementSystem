using Leasing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Domain.Services
{
    public class LeaseService
    {
        public void ValidateCreate(Lease lease)
        {
            if (lease == null)
                throw new ArgumentNullException(nameof(lease));
            if (lease.StartDate >= lease.EndDate)
                throw new InvalidOperationException("Start date must be before end date.");
            if (lease.RentAmount <= 0)
                throw new InvalidOperationException("Rent amount must be positive.");
            // Add more business rules as needed
        }
    }
}
