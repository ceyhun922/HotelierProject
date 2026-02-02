using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;

namespace Service.Concrete
{
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomDal;

        public RoomManager(IRoomDal roomDal)
        {
            _roomDal = roomDal;
        }

        public async Task<List<Room>> GetALLServiceAsync()
        {
            return await _roomDal.GetALLAsync();
        }

        public async Task<List<Room>> GetAllServiceFilterAsync(Expression<Func<Room, bool>> filter)
        {
            return await _roomDal.GetAllFilterAsync(filter);
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _roomDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Room t)
        {
            await _roomDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Room t)
        {
            await _roomDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Room t)
        {
            await _roomDal.UpdateAsync(t);
        }
    }
}