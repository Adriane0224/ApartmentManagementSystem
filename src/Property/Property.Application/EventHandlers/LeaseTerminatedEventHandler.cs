using ApartmentManagement.Contracts.Services;
using MediatR;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;

namespace Property.Application.EventHandlers
{
    public class LeaseTerminatedEventHandler : INotificationHandler<LeaseTerminatedIntegrationEvent>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeaseTerminatedEventHandler(IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork)
        {
            _apartmentRepository = apartmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LeaseTerminatedIntegrationEvent notification, CancellationToken ct)
        {
            var apartment = await _apartmentRepository.GetByIdForUpdateAsync(
                new ApartmentId(notification.ApartmentId), ct);
            if (apartment is null) return;

            var service = new ApartmentStatusService();
            var vacant = service.MarkAsVacant(apartment);

            await _apartmentRepository.UpdateAsync(vacant, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
