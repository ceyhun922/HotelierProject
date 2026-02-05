using System.Threading.Tasks;
using DTOs.UserDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users =await _userManager.Users.Select(dto => new ResultUserDto
            {
                 Id = dto.Id,
                 UserName =dto.UserName,
                 Email =dto.Email
            }).ToListAsync();
            return Ok(users);
        }
    }
}