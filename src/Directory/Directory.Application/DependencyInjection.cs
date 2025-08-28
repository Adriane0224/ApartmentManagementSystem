using Directory.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Directory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDirectoryApplication(this IServiceCollection services)
        {
            services.AddScoped<ITenantCommand, TenantCommand>();
            return services;
        }
    }
}
