using System.Threading.Tasks;
using AutoMapper;
using DTOs.RoleDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> RoleList()
        {
            var values = await _roleManager.Roles.ToListAsync();

            var mapper = _mapper.Map<List<ResultRoleDto>>(values);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());

            if (value == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }

            var mapper = _mapper.Map<GetByIdRoleDto>(value);

            return Ok(mapper);

        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto dto)
        {
            var mapper = _mapper.Map<Role>(dto);

            await _roleManager.CreateAsync(mapper);

            var adminUser = await _userManager.FindByEmailAsync("admin@a.a");
            if (adminUser != null)
            {
                await _userManager.AddToRoleAsync(adminUser, mapper.Name);
            }
            return Ok(new { message = "Eklendi ve Admin'e atandı" });

           
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto dto)
        {
            var value = await _roleManager.FindByIdAsync(dto.Id.ToString());
            if (value == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }

            _mapper.Map(dto, value);

            await _roleManager.UpdateAsync(value);

            return Ok(new { message = "Güncellendi" });

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());

            if (value == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }
            await _roleManager.DeleteAsync(value);

            return Ok(new { message = "Silindi" });

        }


    }
}