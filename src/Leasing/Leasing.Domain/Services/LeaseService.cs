using Leasing.Domain.Entities;

namespace Leasing.Domain.Services
{
    public class LeaseService
    {
        public void ValidateCreate(Lease lease)
        {
            if (lease is null)
                throw new ArgumentNullException(nameof(lease));

            if (lease.StartDate >= lease.EndDate)
                throw new InvalidOperationException("Start date must be before end date.");

            if (lease.MonthlyRent <= 0)
                throw new InvalidOperationException("Monthly rent must be positive.");

            if (lease.SecurityDeposit < 0)
                throw new InvalidOperationException("Security deposit cannot be negative.");
        }
    }
}
