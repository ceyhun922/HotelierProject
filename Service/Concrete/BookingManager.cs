using System.Linq.Expressions;
using DAL.Abstract;
using EntityLayer.Concrete;
using Service.Abstract;

namespace Service.Concrete
{
    public class BookingManager : IBookingService
    {
        private readonly IBookingDal _bookingDal;

        public BookingManager(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public async Task<List<Booking>> GetALLServiceAsync()
        {
            return await _bookingDal.GetALLAsync();
        }

        public async Task<List<Booking>> GetAllServiceFilterAsync(Expression<Func<Booking, bool>> filter)
        {
            return await _bookingDal.GetAllFilterAsync(filter);

        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _bookingDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Booking t)
        {
           await _bookingDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Booking t)
        {
            await _bookingDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Booking t)
        {
            await _bookingDal.UpdateAsync(t);
        }
    }
}