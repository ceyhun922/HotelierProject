using DAL.Abstract;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;

namespace DAL.Entityframework
{
    public class EFChargeRepository : GenericRepository<Charge>, IChargeDal
    {
        private readonly ApiContext _context;
        public EFChargeRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}