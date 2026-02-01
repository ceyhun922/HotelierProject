using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Hotelier.EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFSliderRepository : GenericRepository<Slider>,ISliderDal
    {
          private readonly ApiContext _context;

        public EFSliderRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}