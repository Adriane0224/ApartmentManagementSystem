using Billing.Application.Commands;
using Billing.Application.Queries;
using Billing.Application.Response;
using Billing.Controller.Request;
using Billing.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using static Billing.Application.Commands.GenerateInvoicesCommands;
using static Billing.Application.Commands.RecordPaymentCommands;

namespace Billing.Controller
{
    [ApiController]
    [Route("api/billing")]
    public sealed class BillingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IInvoiceQueries _queries;
        private readonly IPaymentRepository _payments;

        public BillingController(IMediator mediator, IInvoiceQueries queries, IPaymentRepository payments)
        {
            _mediator = mediator;
            _queries = queries;
            _payments = payments;
        }

        [HttpPost("invoices/generate")]
        public async Task<IActionResult> Generate([FromQuery] string period, CancellationToken ct)
        {
            var result = await _mediator.Send(new GenerateInvoicesForPeriodCommand(period), ct);
            return result.IsFailed ? BadRequest(result.Errors.First().Message) : Ok(result.Value);
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> GetInvoices([FromQuery] Guid? tenantId, [FromQuery] Guid? leaseId, [FromQuery] string? status, CancellationToken ct)
        {
            var list = await _queries.GetAsync(tenantId, leaseId, status, ct);
            return Ok(list);
        }

        [HttpGet("invoices/overdue")]
        public async Task<IActionResult> GetOverdue([FromQuery] DateOnly? asOf, CancellationToken ct)
        {
            var date = asOf ?? DateOnly.FromDateTime(DateTime.UtcNow);
            var list = await _queries.GetOverdueAsync(date, ct);
            return Ok(list);
        }

        [HttpPost("payments")]
        public async Task<IActionResult> RecordPayment([FromBody] RecordPaymentRequest body, CancellationToken ct)
        {

            var cmd = new RecordPaymentCommand(
                InvoiceId: body.InvoiceId,
                PayerId: body.PayerId,
                Amount: body.Amount,
                Method: body.Method,
                Reference: body.Reference ?? ""
            );

            var result = await _mediator.Send(cmd, ct);
            return result.IsFailed
                ? BadRequest(result.Errors.First().Message)
                : Ok(result.Value);
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetPayments([FromQuery] Guid payerId, CancellationToken ct)
        {
            var list = await _payments.GetByPayerAsync(payerId, ct);
            return Ok(list.Select(p => new PaymentResponse
            {
                Id = p.Id,
                InvoiceId = p.InvoiceId,
                PayerId = p.PayerId,
                Amount = p.Amount,
                ReceivedAt = p.ReceivedAt,
                Method = p.Method,
                Reference = p.Reference
            }));
        }
    }
}
