using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billing.Application.Response;
using FluentResults;
using MediatR;

namespace Billing.Application.Commands
{
    public static class GenerateInvoicesCommands
    {
        public record GenerateInvoicesForPeriodCommand(string Period)
            : IRequest<Result<List<InvoiceResponse>>>;
    }
}

