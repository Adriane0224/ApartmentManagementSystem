using ApartmentManagement.Contracts.Services;
using Leasing.Domain.DomainEvents;
using MediatR;

namespace Leasing.Application.EventHandler
{
    public class LeaseActivatedEventHandler : INotificationHandler<LeaseActivatedEvent>
    {
        private readonly IEventBus _eventBus;

        public LeaseActivatedEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(LeaseActivatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new LeaseActivatedIntegrationEvent(notification.ApartmentUnitId);

            await _eventBus.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
