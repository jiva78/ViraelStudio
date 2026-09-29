using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViraelStudio.Application.DTOs.Auth;
using ViraelStudio.Infrastructure.Identity;

namespace ViraelStudio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpPost("register")] 
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await userManager.CreateAsync(user,dto.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto) 
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized();

            var result = await userManager.CheckPasswordAsync(user, dto.Password); 
            if(!result) return Unauthorized(); 

            return Ok();
        }
    }

}
