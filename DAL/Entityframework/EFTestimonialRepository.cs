using DAL.GenericRepository;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Hotelier.EntityLayer.Concrete;

namespace DAL.Entityframework
{
    public class EFTestimonialRepository : GenericRepository<Testimonial>, ITestimonialDal
    {
         private readonly ApiContext _context;

        public EFTestimonialRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}