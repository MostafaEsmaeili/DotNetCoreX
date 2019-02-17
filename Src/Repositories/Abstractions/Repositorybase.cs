using DotNetCoreX.Repositories.Interfaces;

namespace DotNetCoreX.Repositories.Abstractions
{
    public abstract class RepositoryBase : IRepository
    {
        public IDbFactory Factory { get; }
        protected RepositoryBase(IDbFactory factory)
        {
            Factory = factory;
        }
    }
}
