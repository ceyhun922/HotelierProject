using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Hotelier.EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFContactRepository : GenericRepository<Contact>, IContactDal
    {
        private readonly ApiContext _context;
        public EFContactRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}