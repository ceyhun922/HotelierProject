using System.Threading.Tasks;
using AutoMapper;
using DTOs.AboutImageDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutImagesController : ControllerBase
    {
        private readonly IAboutImageService _aboutImageService;
        private readonly IMapper _mapper;

        public AboutImagesController(IAboutImageService aboutImageService, IMapper mapper)
        {
            _aboutImageService = aboutImageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AboutImageList()
        {
            var entities = await _aboutImageService.GetALLServiceAsync();

            var mapper = _mapper.Map<List<AboutImage>>(entities);

            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAboutImage(int id)
        {
            var entity = await _aboutImageService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new { messae = "Bulunamadı" });
            }
            var mapper = _mapper.Map<GetByIdAboutImageDto>(entity);
            return Ok(mapper);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAboutImage(UpdateAboutImageDto dto)
        {
            var entity = await _aboutImageService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new { messae = "Bulunamadı" });
            }
            _mapper.Map(dto, entity);

            await _aboutImageService.UpdateServiceAsync(entity);

            return Ok(new { message = "Güncellendi" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutImage(CreateAboutImageDto dto)
        {
            var mapper = _mapper.Map<AboutImage>(dto);

            await _aboutImageService.InsertServiceAsync(mapper);
            return Ok(new { message = "Eklendş" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAboutImage(int id)
        {
            var entity = await _aboutImageService.GetByIdAsync(id);
            
            if (entity == null)
            {
                return Ok(new { messae = "Bulunamadı" });
            }

            await _aboutImageService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});
        }
    }
}