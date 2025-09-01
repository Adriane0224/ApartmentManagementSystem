using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Property.Application.CommandHandler;
using Property.Application.Commands;

namespace Property.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApartmentApplication(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IApartmentCommands, ApartmentCommands>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
