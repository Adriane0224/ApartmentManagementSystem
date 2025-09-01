using ApartmentManagement.Contracts.Services;
using MediatR;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;

namespace Property.Application.CommandHandler
{
    public sealed class LeaseActivatedEventHandler
        : INotificationHandler<LeaseActivatedIntegrationEvent>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeaseActivatedEventHandler(
            IApartmentRepository apartmentRepository,
            IUnitOfWork unitOfWork)
        {
            _apartmentRepository = apartmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            LeaseActivatedIntegrationEvent notification,
            CancellationToken cancellationToken)
        {
            var apartment = await _apartmentRepository.GetByIdForUpdateAsync(
                new ApartmentId(notification.ApartmentUnitId),
                cancellationToken);

            if (apartment is null) return;

            var service = new ApartmentStatusService();
            var occupied = service.Occupy(apartment);

            await _apartmentRepository.UpdateAsync(occupied);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
