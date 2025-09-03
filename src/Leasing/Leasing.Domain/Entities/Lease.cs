using ApartmentManagement.SharedKernel.Entities;
using Leasing.Domain.DomainEvents;
using Leasing.Domain.Exception;
using Leasing.Domain.ValueObject;

namespace Leasing.Domain.Entities
{
    public class Lease : Entity
    {
        public LeaseId Id { get; private set; }
        public Guid ApartmentId { get; private set; } 
        public Guid TenantId { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public decimal MonthlyRent { get; private set; }
        public decimal SecurityDeposit { get; private set; }
        public LeaseStatus Status { get; private set; }

        public enum LeaseStatus { Active = 1, Terminated = 2 }

        protected Lease() { }

        public static Lease Activate(Guid apartmentId, Guid tenantId, DateOnly start, DateOnly end,
                                     decimal monthlyRent, decimal deposit)
        {
            if (end <= start) throw new DateInvalidException("End date must be after start date.");
            if (monthlyRent <= 0) throw new MonthlyRentPositiveException("Monthly rent must be positive.");

            var lease = new Lease
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

            lease.RaiseDomainEvent(new LeaseActivatedEvent(
                lease.Id.Value,
                lease.ApartmentId,    
                lease.StartDate,
                lease.EndDate,
                DateTime.UtcNow)); 

            return lease;
        }

        public void Terminate(DateOnly terminationDate)
        {
            if (Status == LeaseStatus.Terminated) return;
            if (terminationDate < StartDate)
                throw new TerminationException("Termination cannot be before start.");

            Status = LeaseStatus.Terminated;

            RaiseDomainEvent(new LeaseTerminatedEvent(
                Id.Value,
                ApartmentId,
                terminationDate,
                DateTime.UtcNow));
        }

        public void Renew(DateOnly newEndDate, decimal? newMonthlyRent = null)
        {
            if (Status != LeaseStatus.Active)
                throw new InvalidOperationException("Only active leases can be renewed.");
            if (newEndDate <= EndDate)
                throw new ArgumentException("New end date must be after current end date.", nameof(newEndDate));

            EndDate = newEndDate;
            if (newMonthlyRent is > 0) MonthlyRent = newMonthlyRent.Value;
        }
    }
}
