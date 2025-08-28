using Microsoft.Extensions.DependencyInjection;
using Property.Application.CommandHandler;
using Property.Application.Commands;

namespace Property.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApartmentApplication(this IServiceCollection services)
        {
            services.AddScoped<IApartmentCommands, ApartmentCommands>();
            return services;
        }
    }
}
