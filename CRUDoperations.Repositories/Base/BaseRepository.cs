using CRUDoperations.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUDoperations.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public Lazy<AppDbContext> AppDbContext { get; }
        internal BaseRepository(Lazy<AppDbContext> appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public void CreateAsyn(T entity) => AppDbContext.Value.Set<T>().AddAsync(entity);

        public void Update(T entity) => AppDbContext.Value.Set<T>().Update(entity);

        public void Delete(T entity) => AppDbContext.Value.Set<T>().Remove(entity);

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = AppDbContext.Value.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAllAsync() => await AppDbContext.Value.Set<T>().ToListAsync();

    }
}
