using System.Linq.Expressions;
using System.Threading.Tasks;
using Hotelier.DAL.Abstract;
using Hotelier.DAL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.GenericRepository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly ApiContext _context;

        public GenericRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetALLAsync()
        {
          return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllFilterAsync(Expression<Func<T, bool>> filter)
        {
            return  _context.Set<T>().Where(filter).ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }

    }
}