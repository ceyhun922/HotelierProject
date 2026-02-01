using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Hotelier.EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFAboutImageRepository : GenericRepository<AboutImage>, IAboutImageDal
    {
        private readonly ApiContext _context;

        public EFAboutImageRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}