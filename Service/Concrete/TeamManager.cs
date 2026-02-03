using System.Linq.Expressions;
using DAL.Abstract;
using EntityLayer.Concrete;
using Service.Abstract;

namespace Service.Concrete
{
    public class TeamManager : ITeamService
    {
        private readonly ITeamDal _teamDal;

        public TeamManager(ITeamDal teamDal)
        {
            _teamDal = teamDal;
        }

        public async Task<List<Team>> GetALLServiceAsync()
        {
            return await _teamDal.GetALLAsync();
        }

        public async Task<List<Team>> GetAllServiceFilterAsync(Expression<Func<Team, bool>> filter)
        {
            return await _teamDal.GetAllFilterAsync(filter);
        }

        public async Task<Team> GetByIdAsync(int id)
        {
           return await _teamDal.GetByIdAsync(id);
        }

        public async Task InsertServiceAsync(Team t)
        {
           await _teamDal.InsertAsync(t);
        }

        public async Task RemoveServiceAsync(Team t)
        {
           await _teamDal.RemoveAsync(t);
        }

        public async Task UpdateServiceAsync(Team t)
        {
            await _teamDal.UpdateAsync(t);
        }
    }
}