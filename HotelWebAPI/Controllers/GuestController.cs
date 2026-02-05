using System.Threading.Tasks;
using AutoMapper;
using DTOs.GuestDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public GuestController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GuestList()
        {
            var entities =await _bookingService.GetALLServiceAsync();
            var mapper =_mapper.Map<List<ResultGuestDto>>(entities);

            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdGuest(int id)
        {
            var entity =await _bookingService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunmadı"});
            }
            var mapper =_mapper.Map<GetByIdGuestDto>(entity);

            return Ok(mapper);
        }
    }
}