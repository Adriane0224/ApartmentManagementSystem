using ApartmentManagement.SharedKernel;
using MediatR;

namespace ApartmentManagement.Contracts.Services
{
    public class EventBus : IEventBus
    {
        private readonly IPublisher _publiser;

        public EventBus(IPublisher publiser)
        {
            _publiser = publiser;
        }

        public async Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken)
        {
           await _publiser.Publish(integrationEvent, cancellationToken);
        }
    }
}
