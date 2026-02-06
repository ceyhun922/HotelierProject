using DAL.Abstract;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;

namespace DAL.Entityframework
{
    public class EFLocationRepository : GenericRepository<Location>, ILocationDal
    {
        public EFLocationRepository(ApiContext context) : base(context)
        {
        }
    }
}