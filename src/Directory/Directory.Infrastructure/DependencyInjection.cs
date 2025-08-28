using ApartmentManagement.Contracts.Services;
using Directory.Application.Queries;            // 👈 interface
using Directory.Domain.Repositories;
using Directory.Infrastructure.Data;
using Directory.Infrastructure.Data.Repositories;
using Directory.Infrastructure.QueryHandlers; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Directory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDirectoryInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrWhiteSpace(cs))
            {
                services.AddDbContext<DirectoryDbContext>(o =>
                    o.UseSqlServer(cs, sql => sql.MigrationsHistoryTable("_EFMigrationsHistory", "Directory")));
            }

            // Repositories 
            services.AddScoped<ITenantRepository, TenantRepository>();

            // Queries 
            services.AddScoped<ITenantQueries, TenantQueries>();

            // Domain event 
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();

            return services;
        }
    }
}
