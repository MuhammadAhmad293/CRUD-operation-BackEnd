using Microsoft.Extensions.DependencyInjection;

namespace CRUDoperations.Repositories
{
    internal class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>())
        {
        }
    }
}
