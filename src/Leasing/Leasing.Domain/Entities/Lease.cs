using Leasing.Domain.ValueObject;

namespace Leasing.Domain.Entities
{
    public class Lease
    {
        public LeaseId Id { get; private set; }
        public Guid ApartmentId { get; private set; }
        public Guid TenantId { get; private set; } 
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public decimal MonthlyRent { get; private set; }
        public decimal SecurityDeposit { get; private set; }
        public LeaseStatus Status { get; private set; }
        public decimal RentAmount { get; private set; }

        public enum LeaseStatus { Active = 1, Terminated = 2 }

        protected Lease() { }

        public static Lease Activate(Guid apartmentId, Guid tenantId, DateOnly start, DateOnly end,
                                     decimal monthlyRent, decimal deposit)
        {
            if (end <= start) throw new ArgumentException("End date must be after start date.");
            if (monthlyRent <= 0) throw new ArgumentException("Monthly rent must be positive.");

            return new Lease
            {
                Id = new LeaseId(Guid.NewGuid()),
                ApartmentId = apartmentId,
                TenantId = tenantId,
                StartDate = start,
                EndDate = end,
                MonthlyRent = monthlyRent,
                SecurityDeposit = deposit,
                Status = LeaseStatus.Active
            };
        }

        public void Terminate(DateOnly terminationDate)
        {
            if (Status == LeaseStatus.Terminated) return;
            if (terminationDate < StartDate) throw new ArgumentException("Termination cannot be before start.");
            Status = LeaseStatus.Terminated;
        }
    }
}
