using CRUDoperations.Repositories.UnitOfWork;

namespace CRUDoperations.Services.Base
{
    public class BaseService
    {
        protected IUnitOfWork UnitOfWork { get; }
        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
