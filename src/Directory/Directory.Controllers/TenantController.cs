using Directory.Application.Commands;
using Directory.Controllers.Request;
using Microsoft.AspNetCore.Mvc;

namespace Directory.Controllers
{
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantCommand _tenantService;


        public TenantController (ITenantCommand tenantService)
        {
            _tenantService = tenantService;        
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterTenantRequest request, CancellationToken cancellationToken)
        {
            var response = await _tenantService.RegisterAsync(request.Name, request.Email, request.Phone);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
           
        }

        

    }
}
