using System.Text;
using System.Threading.Tasks;
using DTOs.RoomTypeDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomTypeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> RoomTypeList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/RoomTypeControllers");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(jsonData);
                return View(data);

            }
            return View();
        }

        public IActionResult CreateRoomType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoomType(CreateRoomTypeDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7243/api/RoomTypeControllers", content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("RoomTypeList");
        }

        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/RoomTypeControllers?id=" + id);
            return RedirectToAction("RoomTypeList");
        }

        public async Task<IActionResult> UpdateRoomType(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync($"https://localhost:7243/api/RoomTypeControllers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GetByIdRoomTypeDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoomType(UpdateRoomTypeDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7243/api/RoomTypeControllers", content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("RoomTypeList");
        }
    }
}