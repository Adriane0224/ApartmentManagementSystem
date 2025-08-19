using ApartmentManagement.SharedKernel;
using Directory.Domain.Entities;

namespace Directory.Domain.DomainEvents
{
    public record TenantCreatedEvent(Tenant Tenant) : IDomainEvent
    {
        public DateTime OccurredOn => throw new NotImplementedException();
    }
}
