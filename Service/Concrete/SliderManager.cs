using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;

namespace Service.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public async Task<List<Slider>> GetALLServiceAsync()
        {
            return await _sliderDal.GetALLAsync();
        }

        public async Task<List<Slider>> GetAllServiceFilterAsync(Expression<Func<Slider, bool>> filter)
        {
            return await _sliderDal.GetAllFilterAsync(filter);
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _sliderDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Slider t)
        {
            await _sliderDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Slider t)
        {
            await _sliderDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Slider t)
        {
            await _sliderDal.UpdateAsync(t);
        }
    }
}