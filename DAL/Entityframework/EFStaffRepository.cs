using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Hotelier.EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFStaffRepository : GenericRepository<Staff>,IStaffDal
    {
      private readonly ApiContext _context;

        public EFStaffRepository(ApiContext context) : base(context)
        {
            _context =context;
        }   
    }
}