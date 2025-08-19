
using ApartmentManagement.SharedKernel;

namespace ApartmentManagement.Contracts.Services
{
    public interface IEventBus
    {
        Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken);
    }
}
