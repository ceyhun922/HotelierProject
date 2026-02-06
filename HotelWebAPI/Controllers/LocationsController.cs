using System.Threading.Tasks;
using AutoMapper;
using DTOs.LocationDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationsController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> LocationList()
        {
            var entities = await _locationService.GetALLServiceAsync();
            var mapper = _mapper.Map<List<ResultLocationDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdLocation(int id)
        {
            var entity = await _locationService.GetByIdAsync(id);
            if (entity == null) return Ok(new { message = "Bulunamadı" });
            var mapper = _mapper.Map<GetByIdLocationDto>(entity);
            return Ok(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationDto dto)
        {
            var mapper = _mapper.Map<Location>(dto);

            await _locationService.InsertServiceAsync(mapper);

            return Ok(new { message = "Eklendi" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocation(UpdateLocationDto dto)
        {
            var value = await _locationService.GetByIdAsync(dto.Id);
            if (value == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }

            _mapper.Map(dto, value);


            await _locationService.UpdateServiceAsync(value);
            return Ok(new { message = "Güncellendi" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var value = await _locationService.GetByIdAsync(id);
            if (value == null) return Ok(new { message = "Bulunamadı" });

            await _locationService.RemoveServiceAsync(value);

            return Ok(new { message = "Silindi" });

        }
    }
}