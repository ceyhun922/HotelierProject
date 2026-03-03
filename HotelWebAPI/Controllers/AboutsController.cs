using System.Threading.Tasks;
using AutoMapper;
using DTOs.AboutDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;

        public AboutsController(IAboutService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await _aboutService.GetALLServiceAsync();
            var mapper = _mapper.Map<List<ResultAboutDto>>(values);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAbout(int id)
        {
            var value = await _aboutService.GetByIdAsync(id);
            var mapper = _mapper.Map<GetByIdAboutDto>(value);
            return Ok(mapper);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto)
        {
            var entity =await _aboutService.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }

            _mapper.Map(dto, entity);

            await _aboutService.UpdateServiceAsync(entity);

            return Ok(new { message = "Güncellendi" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto)
        {
            var mapper =_mapper.Map<About>(dto);

            mapper.AboutImages = dto.AboutImageUrls?
            .Select(url => new AboutImage { AboutImageUrl = url })
            .ToList();

            await _aboutService.InsertServiceAsync(mapper);

            return Ok(new { message = "Eklendi" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var entity =await _aboutService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }

            await _aboutService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});
        }
    }
}