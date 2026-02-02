using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFAboutRepository : GenericRepository<About>, IAboutDal
    {
        private readonly ApiContext _context;
        public EFAboutRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}