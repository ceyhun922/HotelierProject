using AutoMapper;
using DTOs.StaffDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffControllers : ControllerBase
    {
        private readonly IStafService _stafService;
        private readonly IMapper _mapper;

        public StaffControllers(IMapper mapper, IStafService stafService)
        {
            _mapper = mapper;
            _stafService = stafService;
        }

        [HttpGet]
        public async Task<IActionResult> StaffList()
        {
            var entities =await _stafService.GetALLServiceAsync();

            var mapper =_mapper.Map<List<ResultStaffDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdStaff(int id)
        {
            var entity =await _stafService.GetByIdAsync(id);

            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            var mapper =_mapper.Map<GetByIdStaffDto>(entity);
            return Ok(mapper);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStaff(CreateStaffDto dto)
        {
            var mapper =_mapper.Map<Staff>(dto);

            await _stafService.InsertServiceAsync(mapper);

            return Ok(new {message ="Eklendi"});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(UpdateStaffDto dto)
        {
            var entity =await _stafService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            _mapper.Map(dto,entity);

            await _stafService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var entity =await _stafService.GetByIdAsync(id);
             if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            await _stafService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});

        }

    }
}