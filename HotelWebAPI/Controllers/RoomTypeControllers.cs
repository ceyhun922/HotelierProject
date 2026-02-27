

using AutoMapper;
using DTOs.RoomTypeDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeControllers : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IMapper _mapper;

        public RoomTypeControllers(IRoomTypeService roomTypeService, IMapper mapper)
        {
            _roomTypeService = roomTypeService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> RoomTypeList()
        {
            var entities = await _roomTypeService.GetALLServiceAsync();

            var result = _mapper.Map<List<ResultRoomTypeDto>>(entities);  // Mapper adını daha anlamlı yaptım
            return Ok(result);
        }

      [HttpGet("{id}")]
public async Task<IActionResult> GetByIdRoomType(int id)
{
    var entity = await _roomTypeService.GetByIdAsync(id);

    if (entity == null)
    {
        return NotFound(new { message = "Bulunamadı" });
    }

    var result = _mapper.Map<GetByIdRoomTypeDto>(entity); 
    return Ok(result);
}
       [HttpPost]
public async Task<IActionResult> CreateRoomType(CreateRoomTypeDto dto)
{
    var roomType = _mapper.Map<RoomType>(dto);  

    await _roomTypeService.InsertServiceAsync(roomType);

    return Ok(new { message = "Eklendi" });
}

        [HttpPut]
        public async Task<IActionResult> UpdateRoomType(UpdateRoomTypeDto dto)
        {
            var entity = await _roomTypeService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }

            _mapper.Map(dto, entity);

            await _roomTypeService.UpdateServiceAsync(entity);
            return Ok(new { message = "Güncellendi" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var entity = await _roomTypeService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }
            await _roomTypeService.RemoveServiceAsync(entity);
            return Ok(new { message = "Silindi" });

        }
    }
}