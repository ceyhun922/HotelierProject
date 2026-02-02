using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;

namespace Service.Concrete
{
    public class TestimonialManager : ITestimonialService
    {
        private readonly ITestimonialDal _testimonialDal;

        public TestimonialManager(ITestimonialDal testimonialDal)
        {
            _testimonialDal = testimonialDal;
        }

        public async Task<List<Testimonial>> GetALLServiceAsync()
        {
            return await _testimonialDal.GetALLAsync();
        }

        public async Task<List<Testimonial>> GetAllServiceFilterAsync(Expression<Func<Testimonial, bool>> filter)
        {
            return await _testimonialDal.GetAllFilterAsync(filter);
        }

        public async Task<Testimonial> GetByIdAsync(int id)
        {
            return await _testimonialDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Testimonial t)
        {
            await _testimonialDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Testimonial t)
        {
            await _testimonialDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Testimonial t)
        {
            await _testimonialDal.UpdateAsync(t);
        }
    }
}