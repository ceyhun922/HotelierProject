using DTOs.TeamDtos;
using DTOs.TestimonialDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var res = await client.GetAsync("https://localhost:7243/api/Auth/whoami");

            var res1 = await client.GetAsync("https://localhost:7243/api/Dashboard");

            var jsonData = await res1.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<List<ResultTeamDto>>(jsonData);

            ViewBag.ApiAuthOk = res.IsSuccessStatusCode;
            ViewBag.ApiWhoAmI = await res.Content.ReadAsStringAsync();

            return View(value);
        }
    }
}