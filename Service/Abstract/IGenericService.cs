

using System.Linq.Expressions;

namespace Service.Concrete
{
    public interface IGenericService<T> 
    {
        Task<List<T>> GetALLServiceAsync();
        Task<List<T>> GetAllServiceFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(int id);
        Task InsertServiceAsync(T t);
        Task RemoveServiceAsync(T t);
        Task UpdateServiceAsync(T t);
    }
}