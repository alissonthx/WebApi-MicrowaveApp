using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrowaveApp.WebApi.Models;
using MicrowaveApp.Business;

namespace MicrowaveApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MicrowaveController : ControllerBase
    {
        private readonly Microwave _microwave;

        public MicrowaveController(Microwave microwave)
        {
            _microwave = microwave;
        }

        [HttpPost("start")]
        public IActionResult StartHeating([FromBody] HeatingRequest request)
        {
            try
            {
                _microwave.StartHeating(request.TimeInSeconds, request.Power);
                return Ok(new { Message = "Aquecimento iniciado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }        
}
