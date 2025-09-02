using System;
using AutoMapper;
using Billing.Domain.Ports;
using Billing.Domain.Repositories;
using Billing.Infrastructure.Data;
using Billing.Infrastructure.Data.Repositories;
using Billing.Infrastructure.MappingProfiles;
using Billing.Infrastructure.ReadClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Billing.Application.Queries;
using Billing.Infrastructure.QueryHandlers;

namespace Billing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBillingInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BillingDbContext>(o =>
                o.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "Billing")));

            // repositories + UoW
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // queries
            services.AddScoped<IInvoiceQueries, InvoiceQueries>();

            // AutoMapper
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(BillingMappingProfile).Assembly));

            var leasingBase = config["LeasingApi:BaseUrl"]
                ?? throw new InvalidOperationException("Missing 'LeasingApi:BaseUrl' for Billing.");
            services.AddHttpClient<ILeasingReadPort, LeasingReadClient>(c =>
                c.BaseAddress = new Uri(leasingBase));

            var apartmentsBase = config["ApartmentsApi:BaseUrl"] ?? leasingBase; 
            services.AddHttpClient<IApartmentsReadPort, ApartmentsReadClient>(c =>
                c.BaseAddress = new Uri(apartmentsBase));

            return services;
        }
    }
}
