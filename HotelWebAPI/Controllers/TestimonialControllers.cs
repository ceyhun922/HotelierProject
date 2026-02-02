using AutoMapper;
using DTOs.TestimonialDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialControllers : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IMapper _mapper;

        public TestimonialControllers(ITestimonialService testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

         [HttpGet("list")]
        public async Task<IActionResult> TestimonialList()
        {
            var entities =await _testimonialService.GetALLServiceAsync();

            var mapper =_mapper.Map<List<ResultTestimonialDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTestimonial(int id)
        {
            var entity =await _testimonialService.GetByIdAsync(id);

            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            var mapper =_mapper.Map<GetByIdTestimonialDto>(entity);
            return Ok(mapper);
        }
        [HttpPost("create-testimonial")]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto dto)
        {
            var mapper =_mapper.Map<Testimonial>(dto);

            await _testimonialService.InsertServiceAsync(mapper);

            return Ok(new {message ="Eklendi"});
        }

        [HttpPut("uptade-testimonial")]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto dto)
        {
            var entity =await _testimonialService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            _mapper.Map(dto,entity);

            await _testimonialService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete("delete-tstimonial")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var entity =await _testimonialService.GetByIdAsync(id);
             if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            await _testimonialService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});

        }
    }
}