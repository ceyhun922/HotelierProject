using System.Threading.Tasks;
using DTOs.MessageDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class MailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Inbox()
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync("https://localhost:7243/api/MessageControllers");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> Read(int id)
        {
             var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync($"https://localhost:7243/api/MessageControllers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<GetByIdMessageDto>(jsonData);
                return View(values);
            }
            return View();
        }
        public IActionResult Compose()
        {
            return View();
        }


    }
}