using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using DTOs.BookingDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BookingList()
        {
            var entities = await _bookingService.GetALLServiceAsync();

            var mapper = _mapper.Map<List<ResultBookingDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBooking(int id)
        {
            var entity = await _bookingService.GetByIdAsync(id);

            if (entity == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }

            var mapper = _mapper.Map<GetByIdBookingDto>(entity);
            return Ok(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            var mapper = _mapper.Map<Booking>(dto);

            await _bookingService.InsertServiceAsync(mapper);

            return Ok(new { message = "Eklendi" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto dto)
        {
            var entity = await _bookingService.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }

            _mapper.Map(dto,entity);

            await _bookingService.UpdateServiceAsync(entity);

            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var entity =await _bookingService.GetByIdAsync(id);

             if (entity == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }

            await _bookingService.RemoveServiceAsync(entity);

            return Ok(new {message ="Silindi"});

        }

    }
}