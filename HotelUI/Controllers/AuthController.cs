using System.Text;
using System.Threading.Tasks;
using DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
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
            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                return Content(err);
            }
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Login() => View();
    }
}