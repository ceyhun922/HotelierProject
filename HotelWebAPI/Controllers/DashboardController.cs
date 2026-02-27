using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public DashboardController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var values =await _teamService.GetALLServiceAsync();
              var topValues = values.Take(4).ToList();
            return Ok(topValues);
        }
    }
}