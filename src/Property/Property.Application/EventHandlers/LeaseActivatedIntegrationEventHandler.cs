using ApartmentManagement.Contracts.Services;
using MediatR;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;

namespace Property.Application.EventHandlers
{
    public class LeaseActivatedIntegrationEventHandler
        : INotificationHandler<LeaseActivatedIntegrationEvent>
    {
        private readonly IApartmentRepository _apartments;
        private readonly IUnitOfWork _uow;

        public LeaseActivatedIntegrationEventHandler(IApartmentRepository apartments, IUnitOfWork uow)
        {
            _apartments = apartments;
            _uow = uow;
        }

        public async Task Handle(LeaseActivatedIntegrationEvent msg, CancellationToken ct)
        {
            var apt = await _apartments.GetByIdForUpdateAsync(new ApartmentId(msg.ApartmentUnitId), ct);
            if (apt is null) return;

            var service = new ApartmentStatusService();
            var occupied = service.Occupy(apt);

            await _apartments.UpdateAsync(occupied, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
