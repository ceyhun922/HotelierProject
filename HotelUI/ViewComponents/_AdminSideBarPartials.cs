using Microsoft.AspNetCore.Mvc;

namespace HotelUI.ViewComponents
{
    public class _AdminSideBarPartials : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}