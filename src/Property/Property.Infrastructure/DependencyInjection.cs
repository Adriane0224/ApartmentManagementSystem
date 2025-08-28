using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Property.Application.Queries;
using Property.Domain.Repositories;
using Property.Infrastructure.Data;
using Property.Infrastructure.Data.Repositories;
using Property.Infrastructure.QueryHandlers;

namespace Property.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApartmentInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApartmentDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsHistoryTable("_EFMigrationsHistory", "Apartment"));
            });
            services.AddScoped<IApartmentQueries, ApartmentQueries>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork >();

            return services;
        }
    }
}
