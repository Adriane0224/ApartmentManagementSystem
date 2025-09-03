using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ownership.Application.Commands;
using Ownership.Application.Queries;
using Ownership.Controller.Request;
using static Ownership.Application.Commands.OwnerCommands;

namespace Ownership.Controller
{
    [ApiController]
    [Route("api/ownership")]
    public sealed class OwnershipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOwnerQueries _queries;

        public OwnershipController(IMediator mediator, IOwnerQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }

        [HttpPost("owners")]
        public async Task<IActionResult> Create([FromBody] CreateOwnerRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("Name is required.");

            var result = await _mediator.Send(
                new CreateOwnerCommand(req.Name, req.Email, req.Phone), ct);

            return result.IsFailed
                ? BadRequest(result.Errors.First().Message)
                : Ok(result.Value);
        }

        [HttpPut("owners/{ownerId:guid}")]
        public async Task<IActionResult> Update(Guid ownerId, [FromBody] UpdateOwnerRequest req, CancellationToken ct)
        {
            var result = await _mediator.Send(
                new UpdateOwnerCommand(ownerId, req.Name, req.Email, req.Phone), ct);

            return result.IsFailed
                ? BadRequest(result.Errors.First().Message)
                : Ok(result.Value);
        }

        [HttpGet("owners/{ownerId:guid}")]
        public async Task<IActionResult> Get(Guid ownerId, CancellationToken ct)
        {
            var owner = await _queries.GetOwnerAsync(ownerId, ct);
            return owner is null ? NotFound() : Ok(owner);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignOwnerToUnitRequest req, CancellationToken ct)
        {
            if (req.OwnerId == Guid.Empty || req.UnitId == Guid.Empty)
                return BadRequest("OwnerId and UnitId are required.");

            var result = await _mediator.Send(
                new AssignOwnerToUnitCommand(req.OwnerId, req.UnitId), ct);

            return result.IsFailed
                ? BadRequest(result.Errors.First().Message)
                : Ok(result.Value);
        }

        [HttpDelete("assign/{unitId:guid}")]
        public async Task<IActionResult> Unassign(Guid unitId, CancellationToken ct)
        {
            var result = await _mediator.Send(new UnassignOwnerFromUnitCommand(unitId), ct);
            return result.IsFailed ? BadRequest(result.Errors.First().Message) : NoContent();
        }

        [HttpGet("by-unit/{unitId:guid}")]
        public async Task<IActionResult> GetByUnit(Guid unitId, CancellationToken ct)
        {
            var owner = await _queries.GetOwnerByUnitAsync(unitId, ct);
            return owner is null ? NotFound() : Ok(owner);
        }
    }
}
