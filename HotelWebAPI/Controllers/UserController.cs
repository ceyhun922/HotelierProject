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
            var users = await _userManager.Users.Select(dto => new ResultUserDto
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Email = dto.Email
            }).ToListAsync();
            return Ok(users);
        }

        [HttpPut("auth-update-profile")]
        public async Task<IActionResult> UdateUser(UpdateUserDto dto)
        {
            if (!(User?.Identity?.IsAuthenticated ?? false))
            return Unauthorized(new { message = "Giriş yok" });
           
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Ok(new { message = "Kullanıcı bulunmadı" });
            }

            user.UserName = dto.UserName?.Trim();
            user.Email = dto.Email?.Trim();

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Ok(new { message = "Kullanıcı Bilgileri üncellenmedi" });
            }

            return Ok(new { message = "Kullanıcı Bilgileri Güncellendi" });
        }
    }
}