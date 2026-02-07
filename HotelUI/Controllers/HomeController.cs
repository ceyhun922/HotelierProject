using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}