
using System.Linq.Expressions;
using DAL.Abstract;
using EntityLayer.Concrete;
using Service.Abstract;

namespace Service.Concrete
{
    public class RoomTypeManager : IRoomTypeService
    {
        private readonly IRoomTypeDal _roomTypeDal;

        public RoomTypeManager(IRoomTypeDal roomTypeDal)
        {
            _roomTypeDal = roomTypeDal;
        }

        public async Task<List<RoomType>> GetALLServiceAsync()
        {
            return await _roomTypeDal.GetALLAsync();
        }

        public async Task<List<RoomType>> GetAllServiceFilterAsync(Expression<Func<RoomType, bool>> filter)
        {
            return await _roomTypeDal.GetAllFilterAsync(filter);
        }

        public async Task<RoomType> GetByIdAsync(int id)
        {
            return await _roomTypeDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(RoomType t)
        {
            await _roomTypeDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(RoomType t)
        {
            await _roomTypeDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(RoomType t)
        {
            await _roomTypeDal.UpdateAsync(t);
        }
    }
}