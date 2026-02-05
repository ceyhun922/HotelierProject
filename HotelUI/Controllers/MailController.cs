using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult InboxList()
        {
            return PartialView();
        }

    }
}