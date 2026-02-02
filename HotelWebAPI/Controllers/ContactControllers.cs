using System.Threading.Tasks;
using AutoMapper;
using DTOs.ContactDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Concrete;

namespace HotelWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactControllers : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactControllers(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var entities =await _contactService.GetALLServiceAsync();

            var mapper =_mapper.Map<List<ResultContactDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContact(int id)
        {
            var entity =await _contactService.GetByIdAsync(id);

            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            var mapper =_mapper.Map<GetByIdContactDto>(entity);
            return Ok(mapper);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto dto)
        {
            var mapper =_mapper.Map<Contact>(dto);

            await _contactService.InsertServiceAsync(mapper);

            return Ok(new {message ="Eklendi"});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto dto)
        {
            var entity =await _contactService.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            _mapper.Map(dto,entity);

            await _contactService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var entity =await _contactService.GetByIdAsync(id);
             if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            await _contactService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});

        }
    }
}