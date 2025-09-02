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
    public static class RecordPaymentCommands
    {
        public sealed record RecordPaymentCommand(
            Guid InvoiceId,
            decimal Amount,
            string Method,
            string? Reference,
            Guid? PayerId = null 
        ) : IRequest<Result<PaymentResponse>>;
    }
}

