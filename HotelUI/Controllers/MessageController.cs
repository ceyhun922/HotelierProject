using System.Text;
using DTOs.MessageDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
         public async Task<IActionResult> MessageList()
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync("https://localhost:7243/api/MessageControllers");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(data);
            }
            return View();
        }

        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(dto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            var res =await client.PostAsync("https://localhost:7243/api/MessageControllers",content);
            if (!res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("MessageList");
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7243/api/MessageControllers?id=" + id);
            return RedirectToAction("MessageList");
        }

        public async Task<IActionResult> UpdateMessage(int id)
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync($"https://localhost:7243/api/MessageControllers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<GetByIdMessageDto>(jsonData);
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto dto)
        {
            var client =_httpClientFactory.CreateClient();
            var json =JsonConvert.SerializeObject(dto);
            var content =new StringContent(json,Encoding.UTF8,"application/json");
            var res =await client.PutAsync("https://localhost:7243/api/MessageControllers",content);
            if (! res.IsSuccessStatusCode)
            {
                return View(dto);
            }
            return RedirectToAction("MessageList");
        }
    }
}