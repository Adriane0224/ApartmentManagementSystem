using ApartmentManagement.Contracts.Services; // ensure LeaseActivatedEvent : INotification
using MediatR;
using Property.Domain.Repositories;
using Property.Domain.Services;
using Property.Domain.ValueObject;
using System.Threading;
using System.Threading.Tasks;

public class LeaseActivatedEventHandler : INotificationHandler<LeaseActivatedEvent>
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LeaseActivatedEventHandler(IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork)
    {
        _apartmentRepository = apartmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(LeaseActivatedEvent notification, CancellationToken cancellationToken)
    {
        // Prefer a *tracking* read if your repo supports it
        var apartment = await _apartmentRepository.GetByIdForUpdateAsync(
            new ApartmentId(notification.ApartmentId), cancellationToken); 
        if (apartment is null) return;

        var service = new ApartmentStatusService();
        var occupied = service.Occupy(apartment);          
        await _apartmentRepository.UpdateAsync(occupied); 
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
