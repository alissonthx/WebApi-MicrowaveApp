using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MicrowaveApp.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicrowaveApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            // Credentials validate simple example
            if (login.Username != "admin" || login.Password != "admin123") return Unauthorized();

            var token = GenerateJwtToken(login.Username, login.Password);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username, string password)
        {
            var secretKey = _config["Api:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Api:Issuer"],
                _config["Api:Audience"],
                new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
