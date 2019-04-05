using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.AmbientContext;
using Framework.AccessManagement.Entity;
using Framework.Enum;
using Framework.Repositories;

namespace Framework.AccessManagement.Repository
{
    public class PageResourceRepository : LinqToDbRepositoryBase<PageResource,int> , IPageResourceRepository
    {

        public PageResourceRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }


        public Task<PageResource> GetPageRepositoryByPageName(string pageName)
        {
            try
            {
                return FirstOrDefaultAsync(x => x.Name == pageName && x.Type == ResourceType.Page);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


  
    }
    public interface IPageResourceRepository : IBaseRepository<PageResource, int>
    {
        Task<PageResource> GetPageRepositoryByPageName(string pageName);
    }
}