using System.Threading.Tasks;
using DTOs.GuestDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class GuestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GuestList()
        {
            var client =_httpClientFactory.CreateClient();
            var res =await client.GetAsync("https://localhost:7243/api/Guest");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var data =JsonConvert.DeserializeObject<List<ResultGuestDto>>(jsonData);
                return View(data);
            }
            return View();
        }
    }
}