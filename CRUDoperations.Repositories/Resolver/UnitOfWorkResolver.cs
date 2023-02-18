using CRUDoperations.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUDoperations.Repositories.Resolver
{
    public static class UnitOfWorkResolver
    {
        public static void ResolveUintOfWork(IServiceCollection services, IConfiguration configuration)
        {            
            // Add ConString To Db Context
            services.AddDbContext<Context.AppDbContext>(cnf =>
            {
                cnf.UseSqlServer(configuration.GetConnectionString("DBConString"));
                cnf.UseLazyLoadingProxies(false);
            });

            // Resolve UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        }
        public static void ResolveLazier(IServiceCollection services, IConfiguration configuration)
        {
            // Resolve Lazier
            services.AddScoped(typeof(Lazy<>), typeof(Lazier<>));
        }
    }
}
