using Billing.Application.CommandHandler;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBillingApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BillingCommands).Assembly));
            services.AddScoped<BillingCommands>();
            return services;
        }
    }
}
