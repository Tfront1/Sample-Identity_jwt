using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample_Identity_jwt.Models;
using Sample_Identity_jwt.Services;

namespace Sample_Identity_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginUserDto loginUserDto)
        {
            if (await authService.Register(loginUserDto))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = await authService.Login(loginUserDto);
            if (user != null) {
                var token = authService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest();
        }
    }
}
