using System.Threading.Tasks;
using AutoMapper;
using DTOs.TeamDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamControllers : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamControllers(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Teamlist()
        {
            var entities = await _teamService.GetALLServiceAsync();
            var mapper = _mapper.Map<List<ResultTeamDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTeam(int id)
        {
            var entity = await _teamService.GetByIdAsync(id);
            var mapper = _mapper.Map<GetByIdTeamDto>(entity);
            if (entity == null)
            {
                return Ok(new { message = "Bulunmadı" });
            }
            return Ok(mapper);
        }

        [HttpPut("update-team")]
        public async Task<IActionResult> UpdateTeam(UpdateTeamDto dto)
        {
            var entity =await _teamService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new { message = "Bulunmadı" });
            }
            _mapper.Map(dto, entity);
            await _teamService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }
        [HttpPost("create-team")]
        public async Task<IActionResult> CreateTeam(CreateTeamDto dto)
        {
            var mapper =_mapper.Map<Team>(dto);

            await _teamService.InsertServiceAsync(mapper);
            return Ok(new {message ="Eklendi"});
        }

        [HttpDelete("delete-team")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var entity =await _teamService.GetByIdAsync(id);
             if (entity == null)
            {
                return Ok(new { message = "Bulunmadı" });
            }
            await _teamService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});

        }
    }
}