using DAL.Abstract;
using DAL.GenericRepository;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;

namespace DAL.Entityframework
{
    public class EFTeamRepository : GenericRepository<Team>, ITeamDal
    {
        private readonly ApiContext _context;
        public EFTeamRepository(ApiContext context) : base(context)
        {
            _context =context;
        }
    }
}