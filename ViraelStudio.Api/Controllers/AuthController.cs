using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViraelStudio.Application.DTOs.Auth;
using ViraelStudio.Application.Services;
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

            await userManager.AddToRoleAsync(user, "User");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto, IJwtService jwtService) 
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized();

            var result = await userManager.CheckPasswordAsync(user, dto.Password); 
            if(!result) return Unauthorized();

            var roles = await userManager.GetRolesAsync(user);

            var token = jwtService.GenerateToken(user.Id, user.Email!, roles);

            return Ok(new { token } );
        }
    }

}
