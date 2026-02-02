using System.Linq.Expressions;
using DAL.Abstract;
using EntityLayer.Concrete;
using Service.Abstract;

namespace Service.Concrete
{
    public class ChargeManager : IGenericService<Charge>
    {
        private readonly IChargeDal _chargeDal;

        public ChargeManager(IChargeDal chargeDal)
        {
            _chargeDal = chargeDal;
        }

        public async Task<List<Charge>> GetALLServiceAsync()
        {
            return await _chargeDal.GetALLAsync();
        }

        public async Task<List<Charge>> GetAllServiceFilterAsync(Expression<Func<Charge, bool>> filter)
        {
            return await _chargeDal.GetAllFilterAsync(filter);
        }

        public async Task<Charge> GetByIdAsync(int id)
        {
            return await _chargeDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Charge t)
        {
            await _chargeDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Charge t)
        {
            await _chargeDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Charge t)
        {
            await _chargeDal.UpdateAsync(t);
        }
    }
}