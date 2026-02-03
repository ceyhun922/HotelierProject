using System.Text;
using DTOs.SliderDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelWebAPI.Controllers
{
    public class SliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

               public async Task<IActionResult> SliderList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/SliderControllers");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultSliderDto>>(jsonData);
                return View(data);

            }
            return View();
        }

          public IActionResult CreateSlider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(dto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            var res =await client.PostAsync("https://localhost:7243/api/SliderControllers",content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("SliderList");
        }

        public async Task<IActionResult> DeleteSlider(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/SliderControllers?id=" + id);
            return RedirectToAction("SliderList");
        }

        public async Task<IActionResult> UpdateSlider(int id)
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync($"https://localhost:7243/api/SliderControllers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<GetByIdSliderDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var json =JsonConvert.SerializeObject(dto);
            var content =new StringContent(json,Encoding.UTF8,"application/json");
            var res =await client.PutAsync("https://localhost:7243/api/SliderControllers",content);
            if (! res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("SliderList");
        }
    }
}