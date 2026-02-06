using System.Text;
using System.Threading.Tasks;
using DTOs.RoleDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> RoleList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/Roles");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultRoleDto>>(jsonData);
                return View(data);
            }
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7243/api/Roles", content);
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Başarısız Oldu";
                return View(dto);
            }
            return RedirectToAction("RoleList");
        }

        public async Task<IActionResult> UpdateRole(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync($"https://localhost:7243/api/Roles/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GetByIdRoleDto>(jsonData);
                return View(data);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7243/api/Roles", content);
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Başarısız Oldu";
                return View(dto);
            }
            return RedirectToAction("RoleList");
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/Roles?id=" + id);

            return RedirectToAction("RoleList");

        }


    }
}

