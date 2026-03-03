using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}