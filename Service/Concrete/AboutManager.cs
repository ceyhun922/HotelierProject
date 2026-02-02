using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;

namespace Service.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public async Task<List<About>> GetALLServiceAsync()
        {
            return await _aboutDal.GetALLAsync();
        }

        public async Task<List<About>> GetAllServiceFilterAsync(Expression<Func<About, bool>> filter)
        {
            return await _aboutDal.GetAllFilterAsync(filter);
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _aboutDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(About t)
        {
            await _aboutDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(About t)
        {
            await _aboutDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(About t)
        {
            await _aboutDal.UpdateAsync(t);
        }
    }
}