using System.Linq.Expressions;
using EntityLayer.Concrete;
using Hotelier.DAL.Abstract;
using Service.Abstract;

namespace Service.Concrete
{
    public class StaffManager : IGenericService<Staff>
    {
        public readonly IStaffDal _staffDal;

        public StaffManager(IStaffDal staffDal)
        {
            _staffDal = staffDal;
        }

        public async Task<List<Staff>> GetALLServiceAsync()
        {
            return await _staffDal.GetALLAsync();
        }

        public async Task<List<Staff>> GetAllServiceFilterAsync(Expression<Func<Staff, bool>> filter)
        {
            return await _staffDal.GetAllFilterAsync(filter);
        }

        public async Task<Staff> GetByIdAsync(int id)
        {
            return await _staffDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Staff t)
        {
            await _staffDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Staff t)
        {
            await _staffDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Staff t)
        {
            await _staffDal.UpdateAsync(t);
        }
    }
}