using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync($"https://localhost:7243/api/User/{id}");
            if (!res.IsSuccessStatusCode)
            {
                return View(null);
            }
            var jsonData = await res.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<GetByIdUserDto>(jsonData);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateUserDto dto)
        {
            /*     Console.WriteLine("=== PROFILE UPDATE BAŞLADI ===");
                Console.WriteLine($"DTO Id: {dto.Id}");
                Console.WriteLine($"DTO UserName: {dto.UserName}");
                Console.WriteLine($"DTO Email: {dto.Email}"); */

            var client = _httpClientFactory.CreateClient();

            var sessionToken = HttpContext.Session.GetString("token");
            var cookieToken = Request.Cookies["access_token"];

            /*     Console.WriteLine($"SESSION TOKEN: {(sessionToken != null ? "VAR ✅" : "YOK ❌")}");
                Console.WriteLine($"COOKIE TOKEN: {(cookieToken != null ? "VAR ✅" : "YOK ❌")}");
                 */
            var token = sessionToken ?? cookieToken;

            if (string.IsNullOrWhiteSpace(token))
            {
                /*         Console.WriteLine("❌ HER İKİSİ DE YOK! Login sayfasına yönlendiriliyor.");
                 */
                return RedirectToAction("Login", "Auth");
            }

            Console.WriteLine($"✅ TOKEN BULUNDU: {token.Substring(0, 20)}...");

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);
            Console.WriteLine($"JSON: {jsonData}");

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PutAsync("https://localhost:7243/api/User/auth-update-profile", content);

            Console.WriteLine($"Status Code: {res.StatusCode}");
            var responseBody = await res.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {responseBody}");

            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine("❌ HATA OLUŞTU!");
                ModelState.AddModelError("", responseBody);

                var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(claimId, out var userId))
                {
                    var getClient = _httpClientFactory.CreateClient();
                    var getRes = await getClient.GetAsync($"https://localhost:7243/api/User/{userId}");
                    var json = await getRes.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<GetByIdUserDto>(json);
                    return View(model);
                }
                return View();
            }

            Console.WriteLine("✅ BAŞARILI! Redirect ediliyor...");
            return RedirectToAction(nameof(Index), new { id = dto.Id });
        }

    }
}
