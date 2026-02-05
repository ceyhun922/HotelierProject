using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index() => View();
    }
}