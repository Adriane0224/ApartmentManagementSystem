using ApartmentManagement.SharedKernel;

namespace Leasing.Domain.DomainEvents
{
    public record LeaseTerminatedEvent(
        Guid LeaseId,
        Guid ApartmentUnitId,
        DateOnly TerminationDate,
        DateTime OccurredOn
    ) : IDomainEvent;
}
