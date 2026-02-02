using System.Linq.Expressions;
using DAL.Abstract;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;

namespace DAL.Entityframework
{
    public class EFRoomTypeRepository : GenericRepository<RoomType>, IRoomTypeDal
    {
        private readonly ApiContext _context;

        public EFRoomTypeRepository(ApiContext context) : base(context)
        {
            _context =context;
        }

    }
}