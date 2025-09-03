using ApartmentManagement.Contracts.Services;
using MediatR;

namespace Leasing.Application.EventHandler
{
    public class LeaseDomainEventHandler :
        INotificationHandler<Leasing.Domain.DomainEvents.LeaseActivatedEvent>,
        INotificationHandler<Leasing.Domain.DomainEvents.LeaseTerminatedEvent>
    {
        private readonly IEventBus _eventBus;
        public LeaseDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

        public Task Handle(Leasing.Domain.DomainEvents.LeaseActivatedEvent e, CancellationToken ct)
            => _eventBus.PublishAsync(new LeaseActivatedIntegrationEvent(e.ApartmentUnitId), ct);

        public Task Handle(Leasing.Domain.DomainEvents.LeaseTerminatedEvent e, CancellationToken ct)
            => _eventBus.PublishAsync(new LeaseTerminatedIntegrationEvent(e.ApartmentUnitId), ct);
    }
}
