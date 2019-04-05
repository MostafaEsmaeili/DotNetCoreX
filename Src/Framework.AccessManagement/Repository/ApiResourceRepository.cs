using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.AmbientContext;
using Framework.AccessManagement.Entity;
using Framework.Dapper.Repositories;
using Framework.Enum;
using Framework.Repositories;

namespace Pishkhan.UserManagement.ApiResources
{
    public class ApiResourceRepository : LinqToDbRepositoryBase<ApiResource,int> , IApiResourceRepository
    {
        public ApiResourceRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }
        public Task<ApiResource> GetApiByAddressAndTypeAsync(string apiAddress, string methodType)
        {
            try
            {

                var res = FirstOrDefaultAsync(x =>
                    x.Address == apiAddress && x.MethodType == methodType && x.Type == ResourceType.Api);
                return res;
            }
            catch (Exception e)
            {
                Logger.ErrorException(e.Message,e);
                throw;
            }
        }
    }
    public interface IApiResourceRepository : IBaseRepository<ApiResource,int>
    {
        Task<ApiResource> GetApiByAddressAndTypeAsync(string apiAddress, string methodType);

    }
}