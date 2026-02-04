
using System.Threading.Tasks;
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
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("auth-register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
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

        [HttpPost("auth-login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user =await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Unauthorized(new {message ="Kullanıcı mail veya şifre hatalı"});
            }

            var result =await _signInManager.PasswordSignInAsync(
                user.UserName,
                dto.Password,
                dto.RememberMe,
                lockoutOnFailure: true
            );
            
            if (result.Succeeded)
            {
                return Ok(new {message ="Giriş Başarılı"});
            }

             if (result.IsLockedOut)
                return Unauthorized(new { message = "Hesap kilitlendi. Bir süre sonra tekrar deneyin." });

            return Unauthorized(new { message = "Email veya şifre hatalı." });
        }
    }
}