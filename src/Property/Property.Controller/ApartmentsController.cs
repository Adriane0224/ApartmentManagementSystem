using Microsoft.AspNetCore.Mvc;
using Property.Application.Commands;
using Property.Application.Queries;
using Property.Application.Response;
using Property.Controller.Request;

namespace Property.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentCommands _commands;
        private readonly IApartmentQueries _queries;

        public ApartmentsController(IApartmentCommands commands, IApartmentQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        [HttpPost("addApartment")]
        public async Task<IActionResult> AddApartment([FromBody] AddApartmentRequest request)
        {
            var result = await _commands.AddApartmentAsync(request.UnitNumber, request.Floor, HttpContext.RequestAborted);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First().Message);
            }
            var apartment = result.Value;
            return CreatedAtAction(nameof(GetApartments), new { id = apartment.Id }, apartment);
        }

        //[HttpGet("getAllApartments")]
        //public async Task<IActionResult> GetApartments(string? name)
        //{
        //    ApartmentResponse? apartment = await _queries.GetApartmentByUnitAsync(name);
        //    if (apartment == null)
        //    {
        //        var apartments = await _queries.GetAllApartmentsAsync();
        //        return Ok(apartments);
        //    }
        //    return Ok(apartment);
        //}
        [HttpGet]
        public async Task<IActionResult> GetApartments()
        {
            var apartments = await _queries.GetAllApartmentsAsync();
            return Ok(apartments); 
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var apt = await _queries.GetApartmentByIdAsync(id);
            return apt is null ? NotFound() : Ok(apt);
        }

        [HttpGet("getAllAvailableApartments")]
        public async Task<IActionResult> GetAllAvailableApartments()
        {
            var apartments = await _queries.GetAllAvailableApartments();
            return Ok(apartments);
        }

        //[HttpGet("/getAllUnderMaintenanceApartments")]
        //public async Task<IActionResult> GetUnderMaintenanceApartments(string? name)
        //{
        //    ApartmentResponse? apartment = await _queries.GetApartmentByUnitAsync(name);
        //    if (apartment == null)
        //    {
        //        var apartments = await _queries.GetAllApartmentsAsync();
        //        return Ok(apartment);
        //    }
        //    return Ok(apartment);
        //}

        [HttpPost("{id:guid}/makeAvailable")]
        public async Task<IActionResult> Vacate(Guid id)
        {
            var result = await _commands.VacantApartmentByIdAsync(id, HttpContext.RequestAborted);
            if (result.IsFailed)
                return BadRequest(result.Errors.First().Message);
            return NoContent(); 
        }

        [HttpGet("getAllOccupiedApartments")]
        public async Task<IActionResult> GetOccupiedApartments()
        {
            var apartments = await _queries.GetAllOccupiedApartmentsAsync();
            return Ok(apartments);
        }


        [HttpPost("{id:guid}/occupyUnit")]
        public async Task<IActionResult> OccupyUnit(Guid id)
        {
            var result = await _commands.OccupyApartmentAsync(id, HttpContext.RequestAborted);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First().Message);
            }
            return Ok(result);
        }


        [HttpPost("underMaintenanceUnit")]
        public async Task<IActionResult> UnderMaintainanceUnit(string unit)
        {
            var aparment = await _commands.UnderMaintenanceApartmentAsync(unit, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(GetApartments), new { id = aparment }, aparment);
        }

        [HttpDelete("deleteApartment")]
        public async Task<IActionResult> DeleteApartment(string name)
        {
            var result = await _commands.DeleteApartmentAsync(name, HttpContext.RequestAborted);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First().Message);
            }
            return NoContent();
        }

    }
}
