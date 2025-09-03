using ApartmentManagement.Contracts.Services;
using MediatR;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;

namespace Property.Application.EventHandlers
{
    public class LeaseActivatedEventHandler
        : INotificationHandler<LeaseActivatedIntegrationEvent>
    {
        private readonly IApartmentRepository _repo;
        private readonly IUnitOfWork _uow;

        public LeaseActivatedEventHandler(IApartmentRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }

        public async Task Handle(LeaseActivatedIntegrationEvent notification, CancellationToken ct)
        {
            var apartment = await _repo.GetByIdForUpdateAsync(new ApartmentId(notification.ApartmentUnitId), ct);
            if (apartment is null) return;

            var svc = new ApartmentStatusService();
            var occupied = svc.Occupy(apartment);

            await _repo.UpdateAsync(occupied, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
