using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class ChargeController : Controller
    {
        public IActionResult ChargeList()
        {
            return View();
        }
    }
}