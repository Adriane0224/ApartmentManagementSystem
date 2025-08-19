using ApartmentManagement.SharedKernel;
using MediatR;

namespace ApartmentManagement.Contracts.Services
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IPublisher _publisher;

        public DomainEventPublisher(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task PublishAsync(IEnumerable<IDomainEvent> domainEvent, CancellationToken cancellationToken)
        {

            foreach(var @event in domainEvent)
            {
                await _publisher.Publish(@event);
            }  
        }
    }
}
