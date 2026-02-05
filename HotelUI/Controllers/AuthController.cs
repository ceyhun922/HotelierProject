using System.Text;
using System.Threading.Tasks;
using DTOs.AuthDTOs;
using DTOs.UserDTOs;
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

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
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

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7243/api/Auth/auth-login", content);
            var resJson = await res.Content.ReadAsStringAsync();
            var msg = JsonConvert.DeserializeObject<ApiMessageDto>(resJson);
            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", msg.Message ?? "Giriş Başarısız");
                return View(dto);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var client = _httpClientFactory.CreateClient();

            var res = await client.PostAsync("https://localhost:7243/api/Auth/auth-logout", null);
            if (!res.IsSuccessStatusCode)
            {
                TempData["Error"] = "Logout işlemi başarısız oldu.";
                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}