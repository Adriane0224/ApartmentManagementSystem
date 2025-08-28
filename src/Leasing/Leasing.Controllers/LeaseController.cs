using Leasing.Application.Queries;
using Leasing.Controllers.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Leasing.Application.Commands.CreateLeaseCommands;
using static Leasing.Application.Commands.TerminateLeaseCommands;

namespace Leasing.Controllers
{
    [ApiController]
    [Route("api/leases")]
    public class LeaseController(IMediator mediator, ILeaseQueries queries) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILeaseQueries _queries = queries;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaseRequest req, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreateLeaseCommand(
                req.ApartmentId, req.TenantId, req.StartDate, req.EndDate, req.MonthlyRent, req.SecurityDeposit), ct);

            return result.IsFailed
                ? BadRequest(result.Errors.First().Message)
                : CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var lease = await _queries.GetByIdAsync(id, ct);
            return lease is null ? NotFound() : Ok(lease);
        }

        [HttpPost("{id:guid}/terminate")]
        public async Task<IActionResult> Terminate(Guid id, [FromBody] DateOnly terminationDate, CancellationToken ct)
        {
            var result = await _mediator.Send(new TerminateLeaseCommand(id, terminationDate), ct);
            return result.IsFailed ? BadRequest(result.Errors.First().Message) : NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetActive([FromQuery] Guid? apartmentId, CancellationToken ct)
        {
            var leases = apartmentId.HasValue
                ? await _queries.GetActiveByApartmentAsync(apartmentId.Value, ct)
                : await _queries.GetActiveAsync(ct);
            return Ok(leases);
        }
    }
}
