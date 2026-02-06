using System.Linq.Expressions;
using DAL.Abstract;
using EntityLayer.Concrete;
using Service.Abstract;

namespace Service.Concrete
{
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _locationDal;

        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        public async Task<List<Location>> GetALLServiceAsync()
        {
            return await _locationDal.GetALLAsync();
        }

        public async Task<List<Location>> GetAllServiceFilterAsync(Expression<Func<Location, bool>> filter)
        {
            return await _locationDal.GetAllFilterAsync(filter);
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _locationDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Location t)
        {
            await _locationDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Location t)
        {
            await _locationDal.RemoveAsync(t);

        }

        public async Task UpdateServiceAsync(Location t)
        {
            await _locationDal.UpdateAsync(t);

        }
    }
}