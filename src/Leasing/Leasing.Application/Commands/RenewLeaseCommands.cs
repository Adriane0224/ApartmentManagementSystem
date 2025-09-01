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
    public static class RenewLeaseCommands
    {
        public sealed record RenewLeaseCommand(
            Guid LeaseId,
            DateOnly NewEndDate,
            decimal? NewMonthlyRent
        ) : IRequest<Result<LeaseResponse>>;
    }
}
