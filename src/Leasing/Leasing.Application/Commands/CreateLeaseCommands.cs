using FluentResults;
using Leasing.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Commands
{
    public class CreateLeaseCommands
    {
        public record CreateLeaseCommand(
        Guid ApartmentId,
        Guid TenantId,
        DateOnly StartDate,
        DateOnly EndDate,
        decimal MonthlyRent,
        decimal SecurityDeposit
    ) : IRequest<Result<LeaseResponse>>;
    }
}
