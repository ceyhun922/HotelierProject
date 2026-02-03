using System.Text;
using DTOs.AboutDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> AboutArea()
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync("https://localhost:7243/api/AboutControllers");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(data);
            }
            return View();
        }

        public IActionResult CreateAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(dto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            var res =await client.PostAsync("https://localhost:7243/api/AboutControllers",content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("AboutArea");
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7243/api/AboutControllers/{id}");
            return RedirectToAction("AboutArea");
        }

        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync($"https://localhost:7243/api/AboutControllers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<GetByIdAboutDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var json =JsonConvert.SerializeObject(dto);
            var content =new StringContent(json,Encoding.UTF8,"application/json");
            var res =await client.PutAsync("https://localhost:7243/api/AboutControllers",content);
            if (! res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("AboutArea");
        }
    }
}