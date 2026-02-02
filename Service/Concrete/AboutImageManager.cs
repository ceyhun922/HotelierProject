using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;

namespace Service.Concrete
{
    public class AboutImageManager : IAboutImageService
    {

        private readonly IAboutImageDal _aboutImageDal;

        public AboutImageManager(IAboutImageDal aboutImageDal)
        {
            _aboutImageDal = aboutImageDal;
        }

        public async Task<List<AboutImage>> GetALLServiceAsync()
        {
            return await _aboutImageDal.GetALLAsync();
        }

        public async Task<List<AboutImage>> GetAllServiceFilterAsync(Expression<Func<AboutImage, bool>> filter)
        {
            return await _aboutImageDal.GetAllFilterAsync(filter);
        }

        public async Task<AboutImage> GetByIdAsync(int id)
        {
            return await _aboutImageDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(AboutImage t)
        {
            await _aboutImageDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(AboutImage t)
        {
            await _aboutImageDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(AboutImage t)
        {
            await _aboutImageDal.UpdateAsync(t);
        }
    }
}