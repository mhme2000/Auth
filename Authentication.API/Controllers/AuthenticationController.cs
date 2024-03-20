using Authentication.Application.Contract;
using Authentication.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpGet("{registration}")]
        public async Task<IActionResult> GetByIdAsync(Guid registration)
        {
            var entity = await _authenticationService.FindAsync(registration);
            if (entity == null) return NotFound("User not found.");
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddOrUpdateUserDto request)
        {
            var id = await _authenticationService.AddAsync(request);
            return Created(new Uri($"http://{id}"), new object());
        }

        [HttpPut("{registration}")]
        public async Task<IActionResult> UpdateAsync([FromBody] AddOrUpdateUserDto request, Guid registration)
        {
            var entity = await _authenticationService.UpdateAsync(request, registration);
            if (entity == null) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{registration}")]
        public async Task<IActionResult> DeleteAsync(Guid registration)
        {
            var idResult = await _authenticationService.DeleteAsync(registration);
            if (idResult == null) return BadRequest();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto request)
        {
            var token = await _authenticationService.LoginAsync(request);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("verify-token")]
        public IActionResult VerifyToken()
        {
            return Ok("Valid token");
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now);
        }
    }
}
