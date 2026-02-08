using System.Security.Claims;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelUI.ViewComponents
{
    public class _UserAccontSettingPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UserAccontSettingPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

    
        public async Task<IViewComponentResult> InvokeAsync()
{
    var claimId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (!int.TryParse(claimId, out var id) || id <= 0)
        return View(null);

    var client = _httpClientFactory.CreateClient();
    var res = await client.GetAsync($"https://localhost:7243/api/User/{id}");

    if (!res.IsSuccessStatusCode)
        return View(null);

    var json = await res.Content.ReadAsStringAsync();
    var model = JsonConvert.DeserializeObject<GetByIdUserDto>(json);

    ViewData["UserId"] = model?.Id; // ViewBag yerine ViewData

    return View(model);
}
    }
}
