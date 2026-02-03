using DAL.Abstract;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;

namespace DAL.Entityframework
{
    public class EFMessageRepository : GenericRepository<Message>, IMessageDal
    {
        private readonly ApiContext _context;
        public EFMessageRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}