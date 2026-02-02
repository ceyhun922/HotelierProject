using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFRoomRepository : GenericRepository<Room>, IRoomDal
    {
        private readonly ApiContext _context;

        public EFRoomRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}