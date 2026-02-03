using System.Threading.Tasks;
using AutoMapper;
using DTOs.MessageDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace HotelUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageControllers : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageControllers(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MessageList()
        {
            var entities =await _messageService.GetALLServiceAsync();
            var mapper =_mapper.Map<List<ResultMessageDto>>(entities);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMessage(int id)
        {
            var entity =await _messageService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            var mapper =_mapper.Map<GetByIdMessageDto>(entity);
            return Ok(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto dto)
        {
            var mapper =_mapper.Map<Message>(dto);

            await _messageService.InsertServiceAsync(mapper);
            return Ok(new {message ="Eklendi"});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto dto)
        {
            var entity =await _messageService.GetByIdAsync(dto.Id);

             if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            _mapper.Map(dto,entity);

            await _messageService.UpdateServiceAsync(entity);
            return Ok(new {message ="Güncellendi"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var entity =await _messageService.GetByIdAsync(id);
            if (entity == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }
            await _messageService.RemoveServiceAsync(entity);
            return Ok(new {message ="Silindi"});
            
        }
    }
}