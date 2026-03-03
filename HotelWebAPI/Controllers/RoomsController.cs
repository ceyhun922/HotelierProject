using AutoMapper;
using DTOs.RoomDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
         private readonly IMapper _mapper;

        public RoomsController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

         [HttpGet]
        public async Task<IActionResult> RoomList()
        {
            var entities =await _roomService.GetALLServiceAsync();

            var mapper =_mapper.Map<List<ResultRoomDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdRoom(int id)
        {
            var entity =await _roomService.GetByIdAsync(id);

            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            var mapper =_mapper.Map<GetByIdRoomDto>(entity);
            return Ok(mapper);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomDto dto)
        {
            var mapper =_mapper.Map<Room>(dto);

            await _roomService.InsertServiceAsync(mapper);

            return Ok(new {message ="Eklendi"});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDto dto)
        {
            var entity =await _roomService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            _mapper.Map(dto,entity);

            await _roomService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var entity =await _roomService.GetByIdAsync(id);
             if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            await _roomService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});

        }
    }
}