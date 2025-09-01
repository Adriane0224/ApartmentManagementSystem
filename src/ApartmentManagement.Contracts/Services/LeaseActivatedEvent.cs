using ApartmentManagement.SharedKernel;
using MediatR;

namespace ApartmentManagement.Contracts.Services
{
    public record LeaseActivatedIntegrationEvent(Guid ApartmentUnitId)
        : IIntegrationEvent, INotification;
}
