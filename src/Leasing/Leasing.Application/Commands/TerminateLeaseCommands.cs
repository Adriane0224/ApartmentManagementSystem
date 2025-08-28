using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leasing.Application.Commands
{
    public class TerminateLeaseCommands
    {
        public record TerminateLeaseCommand(Guid LeaseId, DateOnly TerminationDate) : IRequest<Result>;
    }
}
