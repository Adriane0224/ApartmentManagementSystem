using Directory.Application.Commands;
using Directory.Application.Queries;
using Directory.Application.Response;
using Directory.Controllers.Request;
using Microsoft.AspNetCore.Mvc;

namespace Directory.Controllers
{
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantCommand _commands;
        private readonly ITenantQueries _queries;

        public TenantController(ITenantCommand commands, ITenantQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Register([FromBody] RegisterTenantRequest request, CancellationToken cancellationToken)
        {
            var response = await _commands.RegisterAsync(request.Name, request.Email, request.Phone, cancellationToken);
            if (!response.IsSuccess)
                return BadRequest(response.Message);

            return CreatedAtAction(nameof(GetAll), new { }, response);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<TenantResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var tenants = await _queries.GetAllAsync();
            return Ok(tenants);
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<TenantResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var tenant = await _queries.GetTenantByIdAsync(id);
            if (tenant == null)
                return NotFound();
            return Ok(tenant);
        }
    }
}
