using System.Data;

namespace DotNetCoreX.Repositories.Interfaces
{
    public interface ISession : IDbConnection
    {
        IDbConnection Connection { get; }
        IUnitOfWork UnitOfWork();
        IUnitOfWork UnitOfWork(IsolationLevel isolationLevel);
    }
}
