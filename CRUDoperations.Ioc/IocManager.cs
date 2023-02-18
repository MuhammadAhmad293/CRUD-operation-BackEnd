using CRUDoperations.Repositories.Resolver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDoperations.Ioc
{
    public static class IocManager
    {
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            UnitOfWorkResolver.ResolveUintOfWork(services, configuration);
            UnitOfWorkResolver.ResolveLazier(services, configuration);
        }
    }
}