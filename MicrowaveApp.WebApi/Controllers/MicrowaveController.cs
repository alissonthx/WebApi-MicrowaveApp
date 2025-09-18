using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrowaveApp.WebApi.Models;
using MicrowaveApp.Business;
using MicrowaveApp.Business.Services;

namespace MicrowaveApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MicrowaveController : ControllerBase
    {
        private readonly Microwave _microwave;
        private readonly ProgramService _programService;
        private static bool _isRunning = false;
        private static int _currentTime = 0;
        private static int _currentPower = 0;

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

                _isRunning = true;
                _currentTime = request.TimeInSeconds;
                _currentPower = request.Power;

                return Ok(new { Message = "Aquecimento iniciado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            if (!_isRunning)
                return BadRequest("Micro-ondas não está em execução.");

            _microwave.PauseOrCancel(); 
            _isRunning = false;
            _currentTime = 0;
            _currentPower = 0;

            return Ok(new { Message = "Micro-ondas pausado/cancelado." });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new
            {
                IsRunning = _isRunning,
                CurrentTime = _currentTime,
                CurrentPower = _currentPower
            });
        }
    }
}
