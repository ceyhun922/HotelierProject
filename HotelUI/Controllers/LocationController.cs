using System.Text;
using DTOs.LocationDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class LocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> LocationList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/Locations");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
                return View(data);
            }
            return View();
        }

         public IActionResult CreateLocation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(dto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            var res =await client.PostAsync("https://localhost:7243/api/Locations",content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("LocationList");
        }

        public async Task<IActionResult> DeleteLocation(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/Locations?id=" + id);
            return RedirectToAction("LocationList");
        }
        public async Task<IActionResult> UpdateLocation(int id)
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync($"https://localhost:7243/api/Locations/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<GetByIdLocationDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocation(UpdateLocationDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var json =JsonConvert.SerializeObject(dto);
            var content =new StringContent(json,Encoding.UTF8,"application/json");
            var res =await client.PutAsync("https://localhost:7243/api/Locations",content);
            if (! res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("LocationList");
        }
    }


}