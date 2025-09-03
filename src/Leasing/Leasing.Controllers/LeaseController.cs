using Leasing.Application.Queries;
using Leasing.Application.Request;
using Leasing.Controllers.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Leasing.Application.Commands.CreateLeaseCommands;
using static Leasing.Application.Commands.RenewLeaseCommands;
using static Leasing.Application.Commands.TerminateLeaseCommands;

namespace Leasing.Controllers
{
    [ApiController]
    [Route("api/leases")]
    public class LeaseController(IMediator mediator, ILeaseQueries leaseQueries) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILeaseQueries _leaseQueries = leaseQueries;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaseRequest req, CancellationToken ct)
        {
            var result = await _mediator.Send(
                new CreateLeaseCommand(req.ApartmentId, req.TenantId, req.StartDate, req.EndDate, req.MonthlyRent, req.SecurityDeposit),
                ct);

            if (result.IsFailed)
                return BadRequest(result.Errors.First().Message);
            var full = await _leaseQueries.GetByIdAsync(result.Value.Id, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, full);
        }


        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var lease = await _leaseQueries.GetByIdAsync(id, ct);
            return lease is null ? NotFound() : Ok(lease);
        }

        [HttpPost("terminate/{id:guid}")]
        public async Task<IActionResult> Terminate(Guid id, [FromBody] TerminateLeaseRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new TerminateLeaseCommand(id, request.TerminationDate), ct);
            return result.IsFailed ? BadRequest(result.Errors.First().Message) : NoContent();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetActive([FromQuery] Guid? apartmentId, CancellationToken ct)
        {
            var leases = apartmentId.HasValue
                ? await _leaseQueries.GetActiveByApartmentAsync(apartmentId.Value, ct)
                : await _leaseQueries.GetActiveAsync(ct);
            return Ok(leases);
        }

        [HttpPost("renew/{id:guid}")]
        public async Task<IActionResult> Renew(Guid id, [FromBody] RenewLeaseRequest req, CancellationToken ct)
        {
            var result = await _mediator.Send(new RenewLeaseCommand(id, req.NewEndDate, req.NewMonthlyRent), ct);
            return result.IsFailed ? BadRequest(result.Errors.First().Message) : Ok(result.Value);
        }
    }
}
