using System.Threading.Tasks;
using DTOs.LocationDTOs;
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
            var location = await client.GetAsync("https://localhost:7243/api/Locations");

            if (roomtype.IsSuccessStatusCode)
            {
                var jsonData1 = await roomtype.Content.ReadAsStringAsync();
                var data1 = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(jsonData1);
                ViewBag.Select = data1;
            }
            if (location.IsSuccessStatusCode)
            {
                var jsonData2 = await location.Content.ReadAsStringAsync();
                var data2 = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData2);
                ViewBag.Select2 = data2;
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
            var res2 = await client.GetAsync("https://localhost:7243/api/Locations");

            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(jsonData);
                ViewBag.RoomType = new SelectList(list, "Id", "Type");
            }
            if (res2.IsSuccessStatusCode)
            {
                var jsonData1 = await res2.Content.ReadAsStringAsync();
                var list1 = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData1);
                ViewBag.Location = new SelectList(list1, "Id", "Name");
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

    var roomRes = await client.GetAsync($"https://localhost:7243/api/RoomControllers/{id}");
    var typeRes = await client.GetAsync("https://localhost:7243/api/RoomTypeControllers");
    var locRes  = await client.GetAsync("https://localhost:7243/api/Locations");

    if (typeRes.IsSuccessStatusCode)
    {
        var typeJson = await typeRes.Content.ReadAsStringAsync();
        var types = JsonConvert.DeserializeObject<List<ResultRoomTypeDto>>(typeJson);
        ViewBag.RoomType = new SelectList(types, "Id", "Type");
    }

    if (locRes.IsSuccessStatusCode)
    {
        var locJson = await locRes.Content.ReadAsStringAsync();
        var locs = JsonConvert.DeserializeObject<List<ResultLocationDto>>(locJson);
        ViewBag.Location = new SelectList(locs, "Id", "Name");
    }

    if (!roomRes.IsSuccessStatusCode)
        return RedirectToAction("RoomList");

    var roomJson = await roomRes.Content.ReadAsStringAsync();
    var room = JsonConvert.DeserializeObject<GetByIdRoomDto>(roomJson);

    return View(room);
}


        [HttpPost]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            await client.PutAsJsonAsync("https://localhost:7243/api/RoomControllers", dto);
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