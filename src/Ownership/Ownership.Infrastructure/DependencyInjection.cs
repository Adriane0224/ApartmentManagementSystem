using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ownership.Domain.Repositories;
using Ownership.Infrastructure.Data;
using Ownership.Infrastructure.Data.Repositories;
using Ownership.Infrastructure.MappingProfiles;
using Ownership.Application.Queries;
using Ownership.Infrastructure.QueryHandlers;

namespace Ownership.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOwnershipInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<OwnershipDbContext>(o =>
                o.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Ownership")));

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerUnitRepository, OwnerUnitRepository>();
            services.AddScoped<Ownership.Domain.Repositories.IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(OwnershipMappingProfile).Assembly));
            services.AddScoped<IOwnerQueries, OwnerQueries>();

            return services;
        }
    }
}
