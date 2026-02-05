using DAL.Abstract;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;

namespace DAL.Entityframework
{
    public class EFBookingRepository : GenericRepository<Booking>, IBookingDal
    {
        public EFBookingRepository(ApiContext context) : base(context)
        {
        }
    }
}