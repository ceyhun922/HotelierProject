
using DTOs.AuthDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("auth-register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user =new User
            {
              UserName =Guid.NewGuid().ToString(),
              Email =dto.Email
            };

            var result = await _userManager.CreateAsync(user,dto.Password);
            if (result.Succeeded)
            {
                return Ok(new {message ="Kayıt Başarılı"});
            }

            return Ok();
            
        }
    }
}