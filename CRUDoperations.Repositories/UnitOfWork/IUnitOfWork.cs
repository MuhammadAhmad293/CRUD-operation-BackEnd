using CRUDoperations.Repositories.Context;
using CRUDoperations.Repositories.User;

namespace CRUDoperations.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Main Methods
        Task<int> CommitAsync();
        #endregion
        
        #region IRepository
        public IUserRepository UserRepository { get; }
        #endregion
    }
}
