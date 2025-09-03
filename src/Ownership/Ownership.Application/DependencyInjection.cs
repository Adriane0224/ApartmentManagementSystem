using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ownership.Application.CommandHandler;

namespace Ownership.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOwnershipApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(OwnerCommandHandler).Assembly));
            services.AddScoped<OwnerCommandHandler>();
            return services;
        }
    }
}
