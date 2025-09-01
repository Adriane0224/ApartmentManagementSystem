using ApartmentManagement.SharedKernel;

namespace Leasing.IntegrationEvent
{
    
    public record LeaseActivatedIntegrationEvent(
        Guid LeaseId,
        Guid ApartmentUnitId,
        DateOnly StartDate,
        DateOnly EndDate
    ) : IIntegrationEvent;
}
