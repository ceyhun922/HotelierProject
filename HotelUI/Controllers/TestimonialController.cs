using System.Text;
using DTOs.TestimonialDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> TestimonialList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7243/api/TestimonialControllers/list");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(data);

            }
            return View();
        }

          public IActionResult CreateTestimonial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(dto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            var res =await client.PostAsync("https://localhost:7243/api/TestimonialControllers/create-testimonial",content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("TestimonialList");
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/TestimonialControllers/delete-Testimonial?id=" + id);
            return RedirectToAction("TestimonialList");
        }

        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync($"https://localhost:7243/api/TestimonialControllers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<GetByIdTestimonialDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var json =JsonConvert.SerializeObject(dto);
            var content =new StringContent(json,Encoding.UTF8,"application/json");
            var res =await client.PutAsync("https://localhost:7243/api/TestimonialControllers/update-testimonial",content);
            if (! res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("TestimonialList");
        }
    }
}