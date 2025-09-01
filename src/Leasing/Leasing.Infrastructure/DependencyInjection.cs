using Leasing.Application.CommandHandler;
using Leasing.Application.Commands;
using Leasing.Application.Queries;
using Leasing.Domain.Repositories;
using Leasing.Infrastructure.Data;
using Leasing.Infrastructure.Data.Repositories;
using Leasing.Infrastructure.MappingProfiles;
using Leasing.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System; // for Uri

public static class DependencyInjection
{
    public static IServiceCollection AddLeasingInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<LeasingDbContext>(o =>
            o.UseSqlServer(
                config.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Leasing")));

        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(LeaseMappingProfile).Assembly));

        // ✅ Validate config before creating Uri
        var baseUrl = config["TenantApi:BaseUrl"];
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException(
                "Missing 'TenantApi:BaseUrl'. Add it to appsettings (API host) or env vars.");

        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var tenantApiUri))
            throw new InvalidOperationException(
                $"Invalid TenantApi:BaseUrl '{baseUrl}'. Must be an absolute URL.");

        services.AddHttpClient("TenantApi", c => c.BaseAddress = tenantApiUri);

        services.AddScoped<ILeaseRepository, LeaseRepository>();
        services.AddScoped<ILeaseQueries, LeaseQueries>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register the interface, not just the concrete
        services.AddScoped<ILeaseCommands, LeaseCommands>();

        return services;
    }
}
