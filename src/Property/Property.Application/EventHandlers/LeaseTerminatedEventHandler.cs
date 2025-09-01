using ApartmentManagement.Contracts.Services;
using MediatR;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;

namespace Property.Application.EventHandlers
{
    public sealed class LeaseTerminatedEventHandler
        : INotificationHandler<LeaseTerminatedIntegrationEvent>
    {
        private readonly IApartmentRepository _repo;
        private readonly IUnitOfWork _uow;

        public LeaseTerminatedEventHandler(IApartmentRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }

        public async Task Handle(LeaseTerminatedIntegrationEvent notification, CancellationToken ct)
        {
            var apartment = await _repo.GetByIdForUpdateAsync(new ApartmentId(notification.ApartmentUnitId), ct);
            if (apartment is null) return;

            var svc = new ApartmentStatusService();
            var vacant = svc.MarkAsVacant(apartment);

            await _repo.UpdateAsync(vacant, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
