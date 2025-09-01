using ApartmentManagement.SharedKernel;

namespace Leasing.Domain.DomainEvents
{
    public record LeaseActivatedEvent(
        Guid LeaseId,
        Guid ApartmentUnitId,
        DateOnly StartDate,
        DateOnly EndDate
    ) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
