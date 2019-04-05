using System.Diagnostics.Contracts;
using Dapper.AmbientContext;
using Framework.Repositories;

namespace Framework.Sampe.Console
{
    public class ContractRepository : LinqToDbRepositoryBase<Cotnract, int>, IContractRepository
    {
	    public ContractRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
	    {
	    }

    }
}