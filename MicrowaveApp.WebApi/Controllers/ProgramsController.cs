using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrowaveApp.Business.Models;
using MicrowaveApp.Business.Services;

namespace MicrowaveApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgramsController : ControllerBase
    {
        private readonly ProgramService _programService;

        public ProgramsController(ProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet("predefined")]
        public IActionResult GetPredefinedPrograms()
        {
            return Ok(_programService.GetPredefinedPrograms());
        }

        [HttpGet("custom")]
        public IActionResult GetCustomPrograms()
        {
            return Ok(_programService.GetCustomPrograms());
        }

        [HttpPost("custom")]
        public IActionResult AddCustomProgram([FromBody] CustomProgram program)
        {
            try
            {
                _programService.AddCustomProgram(program);
                return CreatedAtAction(nameof(GetCustomPrograms), null);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
