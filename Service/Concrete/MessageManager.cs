using System.Linq.Expressions;
using DAL.Abstract;
using EntityLayer.Concrete;
using Service.Abstract;

namespace Service.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public async Task<List<Message>> GetALLServiceAsync()
        {
            return await _messageDal.GetALLAsync();
        }

        public async Task<List<Message>> GetAllServiceFilterAsync(Expression<Func<Message, bool>> filter)
        {
            return await _messageDal.GetALLAsync();
            
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _messageDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Message t)
        {
           await _messageDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Message t)
        {
            await _messageDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Message t)
        {
           await _messageDal.UpdateAsync(t);
        }
    }
}