using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Hotelier.EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFServiceRepository : GenericRepository<Service>, IServiceDal
    {
          private readonly ApiContext _context;

        public EFServiceRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}