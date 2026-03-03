using System.Text;
using System.Threading.Tasks;
using DTOs.RoleDTOs;
using DTOs.UserDTOs;
using HotelUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace HotelUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAssignController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        // URL'yi burada merkezi bir değişken olarak tanımlıyoruz
        private readonly string _baseUrl = "https://localhost:7243/api";

        public RoleAssignController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private string GetToken() => Request.Cookies["access_token"];

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            // Eğer API korumalıysa token ekliyoruz
            var token = GetToken();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // URL artık tam adres (Absolute URI)
            var res = await client.GetAsync($"{_baseUrl}/User");

            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(data);
            }
            return View(new List<ResultUserDto>());
        }

        public async Task<IActionResult> RoleAuthority(int id)
        {
            var client = _httpClientFactory.CreateClient();
            string baseUrl = "https://localhost:7243/api";

            // 1. TOKEN'I AL VE HEADER'A EKLE (En önemli kısım burası!)
            var token = GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // 2. KULLANICIYI ÇEK
            var resUser = await client.GetAsync($"{baseUrl}/User/{id}");
            if (!resUser.IsSuccessStatusCode)
            {
                return Content($"HATA: Kullanıcı bulunamadı. Status: {resUser.StatusCode}");
            }
            var userJson = await resUser.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<GetByIdUserDto>(userJson);

            // 3. TÜM ROLLERİ ÇEK (Admin yetkisi ister)
            var resRoles = await client.GetAsync($"{baseUrl}/Roles");
            if (!resRoles.IsSuccessStatusCode)
            {
                return Content($"HATA: Roller listelenemedi. Yetkiniz olmayabilir veya Token hatalı. Status: {resRoles.StatusCode}");
            }
            var rolesJson = await resRoles.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<ResultRoleDto>>(rolesJson);

            // 4. KULLANICININ ŞU ANKİ ROLLERİNİ ÇEK
            var resUserRoles = await client.GetAsync($"{baseUrl}/RoleAssigns/user/{id}/roles");
            List<string> userRoles = new List<string>();
            if (resUserRoles.IsSuccessStatusCode)
            {
                var userRolesJson = await resUserRoles.Content.ReadAsStringAsync();
                userRoles = JsonConvert.DeserializeObject<List<string>>(userRolesJson) ?? new List<string>();
            }

            var viewModel = new RoleAuthorityViewModel
            {
                User = user,
                Roles = roles,
                UserId = id,
                UserRoles = userRoles
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> RoleAuthority(string userId, List<string> selectedRoles) // DİKKAT: int değil string yaptık!
        {
            var client = _httpClientFactory.CreateClient();
            var token = GetToken();

            // 1. Güvenlik Kontrolü
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Auth");

            // 2. Token'ı Header'a ekle
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 3. API'nin beklediği paketi (DTO) hazırla
            var dto = new
            {
                UserId = userId,
                Roles = selectedRoles ?? new List<string>()
            };

            // 4. Veriyi JSON formatına çevir
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // 5. API'ye POST isteği gönder
            var res = await client.PostAsync($"{_baseUrl}/RoleAssigns/assign-roles", content);

            // 6. Sonuç Kontrolü
            if (res.IsSuccessStatusCode)
            {
                TempData["Success"] = "Roller başarıyla güncellendi.";
                return RedirectToAction(nameof(RoleAuthority), new { id = userId });
            }

            // --- Hata varsa detayını yakala ---
            var errorDetail = await res.Content.ReadAsStringAsync();

            // Eğer API mesaj göndermediyse Status Code yazdıralım
            if (string.IsNullOrWhiteSpace(errorDetail))
            {
                errorDetail = $"API Hata kodu döndürdü: {res.StatusCode}";
            }

            TempData["Error"] = "API Hatası: " + errorDetail;
            return RedirectToAction(nameof(RoleAuthority), new { id = userId });
        }
    }
}