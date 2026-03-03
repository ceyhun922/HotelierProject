using Microsoft.AspNetCore.Mvc;
using DTOs.AssignRoleDTOs;
using Microsoft.AspNetCore.Identity;
using EntityLayer.Concrete;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleAssignsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public RoleAssignsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

       [HttpPost("assign-roles")]
        public async Task<IActionResult> AssignRoles([FromBody] AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());

            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı Bulunamadı" });
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                var removeRole = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeRole.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "Mevcut roller temizlenemedi",
                        errors = removeRole.Errors.Select(e => e.Description)
                    });
                }
            }

            if (dto.Roles != null && dto.Roles.Any())
            {
                var addRoleResult = await _userManager.AddToRolesAsync(user, dto.Roles);
                if (!addRoleResult.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "Roller eklenemedi",
                        errors = addRoleResult.Errors.Select(e => e.Description)
                    });
                }
            }

            return Ok(new { message = "Roller başarıyla güncellendi", success = true });
        }

        [HttpGet("user/{id}/roles")]
        public async Task<IActionResult> GetUserRoles(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı Bulunamadı" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles.ToList());
        }
    }
}