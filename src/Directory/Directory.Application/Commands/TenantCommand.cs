using ApartmentManagement.Contracts.Services;
using Directory.Domain.Entities;
using Directory.Domain.Repositories;
using Identity.Application.Response;

namespace Directory.Application.Commands
{
    public class TenantCommand : ITenantCommand
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IDomainEventPublisher _publisher;

        public TenantCommand(ITenantRepository tenantRepository, IDomainEventPublisher publisher)
        {
            _tenantRepository = tenantRepository;
            _publisher = publisher;
        }

        public async Task<TenantRegistrationResponse> RegisterAsync(string name, string email, string? phone, CancellationToken cancellationToken)
        {
            // Check if tenant already exists by email
            var existing = await _tenantRepository.GetByEmailAsync(email);
            if (existing is not null)
            {
                return new TenantRegistrationResponse
                {
                    IsSuccess = false,
                    Message = "Tenant already exists with this email."
                };
            }

            // Create + persist
            var tenant = Tenant.Create(name, email, phone);
            await _tenantRepository.AddAsync(tenant);
            await _tenantRepository.SaveChangesAsync(default);

            // Publish domain events (if any were raised)
            await _publisher.PublishAsync(tenant.DomainEvents, default);

            return new TenantRegistrationResponse
            {
                IsSuccess = true,
                Message = "Tenant registered successfully.",
                TenantId = tenant.Id.Value
            };
        }
    }
}
