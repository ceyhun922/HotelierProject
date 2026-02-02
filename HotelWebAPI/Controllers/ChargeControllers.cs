using System.Threading.Tasks;
using AutoMapper;
using DTOs.ChargeDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargeControllers : ControllerBase
    {
        private readonly IChargeService _chargeService;
        private readonly IMapper _mapper;

        public ChargeControllers(IChargeService chargeService, IMapper mapper)
        {
            _chargeService = chargeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ChargeList()
        {
            var entities = await _chargeService.GetALLServiceAsync();
            var mapper = _mapper.Map<List<ResultChargeDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCharge(int id)
        {
            var entity = await _chargeService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new { messae = "Bulunamadı" });
            }
            var mapper = _mapper.Map<GetByIdChargeDto>(entity);
            return Ok(mapper);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharge(UpdateChargeDto dto)
        {
            var entity = await _chargeService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new { messae = "Bulunamadı" });
            }
            _mapper.Map(dto, entity);
            await _chargeService.UpdateServiceAsync(entity);
            return Ok(new { message = "Güncellendi" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharge(int id)
        {
            var entity = await _chargeService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new { messae = "Bulunamadı" });
            }
            await _chargeService.RemoveServiceAsync(entity);
            return Ok(new { message = "Silindi" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharge(CreateChargeDto dto)
        {
            var entity = _mapper.Map<Charge>(dto);
            await _chargeService.InsertServiceAsync(entity);
            return Ok(new { message = "Eklendi" });
        }
    }
}