using System.Threading.Tasks;
using DTOs.RoomDTOs;
using DTOs.RoomTypeDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> RoomList()
        {

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/RoomControllers");
            var roomtype = await client.GetAsync("https://localhost:7243/api/RoomTypeControllers");

            if (roomtype.IsSuccessStatusCode)
            {
                var jsonData1 = await roomtype.Content.ReadAsStringAsync();
                var data1 = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(jsonData1);
                ViewBag.Select = data1;
            }

            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultRoomDto>>(jsonData);
                return View(data);
            }

            return View();
        }

        public async Task<IActionResult> CreateRoom()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/RoomTypeControllers");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(jsonData);
                ViewBag.RoomType = new SelectList(list, "Id", "Type");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            await client.PostAsJsonAsync("https://localhost:7243/api/RoomControllers", dto);
            return RedirectToAction("RoomList");
        }

        public async Task<IActionResult> UpdateRoom(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync($"https://localhost:7243/api/RoomControllers/{id}");
            var res1 = await client.GetAsync("https://localhost:7243/api/RoomTypeControllers");
            if (res1.IsSuccessStatusCode)
            {
                var jsonData1 = await res1.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(jsonData1);
                ViewBag.RoomType = new SelectList(list, "Id", "Type");
            }
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GetByIdRoomDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            await client.PutAsJsonAsync("https://localhost:7243/api/RoomControllers",dto);
            return RedirectToAction("RoomList");
        }

        public async Task<IActionResult> DeleteRoom(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/RoomControllers?id=" + id);
                        return RedirectToAction("RoomList");

            
        }

    }
}