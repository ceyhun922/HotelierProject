using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Authorize]

        public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("api");
        var res = await client.GetAsync("https://localhost:7243/api/Auth/whoami");


        ViewBag.ApiAuthOk = res.IsSuccessStatusCode;
        ViewBag.ApiWhoAmI = await res.Content.ReadAsStringAsync();

        return View();
    }
    }
}