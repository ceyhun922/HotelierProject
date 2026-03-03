using AutoMapper;
using DTOs.SliderDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlidersController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SlidersController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }
                 [HttpGet]
        public async Task<IActionResult> SliderList()
        {
            var entities =await _sliderService.GetALLServiceAsync();

            var mapper =_mapper.Map<List<ResultSliderDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSlider(int id)
        {
            var entity =await _sliderService.GetByIdAsync(id);

            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            var mapper =_mapper.Map<GetByIdSliderDto>(entity);
            return Ok(mapper);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto dto)
        {
            var mapper =_mapper.Map<Slider>(dto);

            await _sliderService.InsertServiceAsync(mapper);

            return Ok(new {message ="Eklendi"});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto dto)
        {
            var entity =await _sliderService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            _mapper.Map(dto,entity);

            await _sliderService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var entity =await _sliderService.GetByIdAsync(id);
             if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            await _sliderService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});

        }
    }
}