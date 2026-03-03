using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DTOs.UserDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, IMapper mapper, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Kullanıcı oluşturulamadı", errors = result.Errors });
            }

            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            if (roleExists)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok(new { message = "Admin kullanıcısı başarıyla eklendi" });
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = new List<ResultUserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userListDto.Add(new ResultUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList() 
                });
            }
            return Ok(userListDto);
        }

        [Authorize]
        [HttpPut("auth-update-profile")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(claimId) || claimId != dto.Id.ToString())
                return Forbid();

            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı" });

            if (!string.IsNullOrWhiteSpace(dto.UserName))
                user.UserName = dto.UserName;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new { message = "Güncelleme başarısız", errors });
            }

            return Ok(new { message = "Profil başarıyla güncellendi" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return NotFound(new { message = "Bulunamadı" });

            var mapper = _mapper.Map<GetByIdUserDto>(user);

            return Ok(mapper);
        }
    }
}