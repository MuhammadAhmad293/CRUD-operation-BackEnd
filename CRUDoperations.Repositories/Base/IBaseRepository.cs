using System.Linq.Expressions;

namespace CRUDoperations.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        void CreateAsyn(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync();
    }
}
