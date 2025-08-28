using Leasing.Application.CommandHandler;
using Leasing.Application.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddLeasingApplication(this IServiceCollection services)
        {
            services.AddScoped<ILeaseCommands, LeaseCommands>();
            return services;
        }
    }
}
