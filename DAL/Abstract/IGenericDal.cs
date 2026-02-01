using System.Linq.Expressions;

namespace Hotelier.DAL.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<List<T>> GetALLAsync();
        Task<List<T>> GetAllFilterAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task InsertAsync(T entity);
        Task<T> GetByIdAsync(int id);
    }
}