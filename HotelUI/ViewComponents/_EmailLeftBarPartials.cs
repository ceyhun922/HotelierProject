using Microsoft.AspNetCore.Mvc;

namespace HotelUI.ViewComponents
{
    public class _EmailLeftBarPartials : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}