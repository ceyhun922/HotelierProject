using System.Threading.Tasks;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> UserList()
        {
            var client =_httpClientFactory.CreateClient();
            
            var res =await client.GetAsync("https://localhost:7243/api/User");
            if (res.IsSuccessStatusCode)
            {
                var jsonData =await res.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(values);
            }
            return View();
        }

       
    }
}