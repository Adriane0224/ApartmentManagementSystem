
using ApartmentManagement.SharedKernel;

namespace ApartmentManagement.Contracts.Services
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken);
    }
}
