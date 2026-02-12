using System.Text;
using System.Threading.Tasks;
using DTOs.RoleDTOs;
using DTOs.UserDTOs;
using HotelUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class RoleAssignController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoleAssignController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/User");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(data);
            }
            return View();
        }

        public async Task<IActionResult> RoleAuthority(int id)
        {
            var client = _httpClientFactory.CreateClient();
            
            var resUser = await client.GetAsync($"https://localhost:7243/api/User/{id}");
            if (!resUser.IsSuccessStatusCode)
            {
                TempData["Error"] = "Kullanıcı bulunamadı";
                return RedirectToAction("Index");
            }

            var jsonData = await resUser.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<GetByIdUserDto>(jsonData);

            var resRoles = await client.GetAsync("https://localhost:7243/api/Roles");
            if (!resRoles.IsSuccessStatusCode)
            {
                TempData["Error"] = "Roller yüklenemedi";
                return RedirectToAction("Index");
            }

            var jsonRoleData = await resRoles.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<ResultRoleDto>>(jsonRoleData);

            var resUserRoles = await client.GetAsync($"https://localhost:7243/api/RoleAssigns/user/{id}/roles");
            List<string> userRoles = new List<string>();
            
            if (resUserRoles.IsSuccessStatusCode)
            {
                var jsonUserRoles = await resUserRoles.Content.ReadAsStringAsync();
                userRoles = JsonConvert.DeserializeObject<List<string>>(jsonUserRoles) ?? new List<string>();
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
        public async Task<IActionResult> RoleAuthority(int userId, List<string> selectedRoles)
        {
            var client = _httpClientFactory.CreateClient();

            var token = Request.Cookies["access_token"];
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction("Login", "Auth");

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var dto = new
            {
                UserId = userId,
                Roles = selectedRoles ?? new List<string>()
            };

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:7243/api/RoleAssigns/assign-roles", content);

            if (res.IsSuccessStatusCode)
            {
                TempData["Success"] = "Roller başarıyla atandı!";
                return RedirectToAction(nameof(RoleAuthority), new { id = userId });
            }

            var error = await res.Content.ReadAsStringAsync();
            TempData["Error"] = "Roller atanamadı: " + error;
            return RedirectToAction(nameof(RoleAuthority), new { id = userId });
        }
    }
}