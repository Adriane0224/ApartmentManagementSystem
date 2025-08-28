using Leasing.Application.CommandHandler;
using Leasing.Application.Queries;
using Leasing.Domain.Repositories;
using Leasing.Infrastructure.Data;
using Leasing.Infrastructure.Data.Repositories;
using Leasing.Infrastructure.MappingProfiles;
using Leasing.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Leasing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLeasingInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LeasingDbContext>(o =>
                o.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Leasing")));

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(LeaseMappingProfile).Assembly));

            services.AddScoped<ILeaseRepository, LeaseRepository>();
            services.AddScoped<ILeaseQueries, LeaseQueries>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<LeaseCommands>();

            return services;
        }
    }
}
