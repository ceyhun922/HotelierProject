
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DTOs.AuthDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            return Ok(new
            {
                isAuth = User.Identity?.IsAuthenticated,
                name = User.Identity?.Name,
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                email = User.FindFirstValue(ClaimTypes.Email)
            });
        }

        [HttpPost("auth-register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new User
            {
                UserName = Guid.NewGuid().ToString(),
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                return Ok(new { message = "Kayıt Başarılı" });
            }

            return BadRequest(new
            {
                message = "Kayıt Başarısız",
                errors = result.Errors.Select(x => x.Description)
            });
        }

        [HttpPost("auth-login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized(new { message = "Kullanıcı mail veya şifre hatalı" });

            var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!check.Succeeded)
                return Unauthorized(new { message = "Email veya şifre hatalı" });

            var token = await CreateJwtAsync(user);
            var expireMinutes = int.Parse(_config["Jwt:ExpireMinutes"]!);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpire = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new LoginResponseDto
            {
                UserId = user.Id,
                AccessToken = token,
                ExpiresIn = expireMinutes * 60,
                RefreshToken = refreshToken
            });
        } 
        private async Task<string> CreateJwtAsync(User user)
        {
            var jwt = _config.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? "")
                };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpireMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Çıkış Yapıldı" });
        }
        
        [HttpPost("refresh")]
public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
{
    var user = _userManager.Users
        .FirstOrDefault(u => u.RefreshToken == dto.RefreshToken);

    if (user == null || user.RefreshTokenExpire < DateTime.UtcNow)
        return Unauthorized(new { message = "Geçersiz veya süresi dolmuş token" });

    var newAccessToken = await CreateJwtAsync(user);
    var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    user.RefreshToken = newRefreshToken;
    user.RefreshTokenExpire = DateTime.UtcNow.AddDays(7);
    await _userManager.UpdateAsync(user);

    var expireMinutes = int.Parse(_config["Jwt:ExpireMinutes"]!);

    return Ok(new LoginResponseDto
    {
        UserId = user.Id,
        AccessToken = newAccessToken,
        ExpiresIn = expireMinutes * 60,
        RefreshToken = newRefreshToken
    });
}
    }
}