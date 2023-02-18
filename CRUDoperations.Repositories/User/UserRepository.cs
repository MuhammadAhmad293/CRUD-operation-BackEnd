using CRUDoperations.Repositories.Base;
using CRUDoperations.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDoperations.Repositories.User
{
    public class UserRepository : BaseRepository<DataModel.Entities.User>, IUserRepository
    {
        public UserRepository(Lazy<AppDbContext> appDbContext) : base(appDbContext)
        {
        }
    }
}
