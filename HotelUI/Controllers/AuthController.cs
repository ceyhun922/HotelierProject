using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTOs.AuthDTOs;
using DTOs.UserDTOs;
using HotelUI.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class ApiMessageDto
    {
        public string? Message { get; set; }
    }
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7243/api/Auth/auth-register", content);

            var resJson = await res.Content.ReadAsStringAsync();
            var msg = JsonConvert.DeserializeObject<ApiMessageDto>(resJson);

            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", msg.Message ?? "Giriş Başarısız");
                return View(dto);
            }

            if (res.IsSuccessStatusCode)
            {
                TempData["Success"] = msg.Message ?? "Giriş Başarılı";
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:7243/api/Auth/auth-login", content);
            var resJson = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Giriş Başarısız: " + resJson);
                return View(dto);
            }

            var loginRes = JsonConvert.DeserializeObject<DTOs.AuthDTOs.LoginResponseDto>(resJson);

            if (loginRes == null || string.IsNullOrWhiteSpace(loginRes.AccessToken))
            {
                ModelState.AddModelError("", "Token alınamadı");
                return View(dto);
            }
            
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(loginRes.AccessToken);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, loginRes.UserId.ToString()),
        new Claim(ClaimTypes.Name, dto.Email),
        new Claim(ClaimTypes.Email, dto.Email)
    };


            var roleClaims = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role || c.Type == "role");
            claims.AddRange(roleClaims);


            var identity = new ClaimsIdentity(claims, "HotelCookie");

            await HttpContext.SignInAsync(
                "HotelCookie", 
                new ClaimsPrincipal(identity)
            );
            Response.Cookies.Append("access_token", loginRes.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddSeconds(loginRes.ExpiresIn)
            });

            HttpContext.Session.SetString("token", loginRes.AccessToken);

            return RedirectToAction("Index", "Dashboard");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("access_token");
            await HttpContext.SignOutAsync("HotelCookie");

            return RedirectToAction("Login", "Auth");
        }


    }
}