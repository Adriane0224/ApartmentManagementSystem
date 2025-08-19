using ApartmentManagement.SharedKernel;
using Directory.Domain.Entities;

namespace Directory.Domain.DomainEvents
{
    public record BuildingCreatedEvent(Tenant Tenant) : IDomainEvent
    {
        public DateTime OccurredOn => throw new NotImplementedException();
    }
}
