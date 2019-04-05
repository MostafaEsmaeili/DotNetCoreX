using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.AmbientContext;
using Framework.AccessManagement.Entity;
using Framework.Enum;
using Framework.Repositories;

namespace Framework.AccessManagement.Repository
{
     public class MenuResourceRepository : LinqToDbRepositoryBase<MenuResource, int>, IMenuResourceRepository
    {
       
        public MenuResourceRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }
        public Task<MenuResource> GetMenuResourceByMenuName(string menuName)
        {
            try
            {
                return FirstOrDefaultAsync(x=>x.Name==menuName && x.Type == ResourceType.Menu);
            }


            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public Task<IEnumerable<MenuResource>> GetMenuByPageId(int pageId)
        {
            try
            {
                return GetAllAsync(x=>x.MenuPageId==pageId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public Task<IEnumerable<MenuResource>> GetAllRootMenu()
        {
            try
            {
                return GetAllAsync(x=>x.Type==ResourceType.Menu && x.ParenMenuResourceId == null);
            }

            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
        public Task<IEnumerable<MenuResource>> GetAllRootMenuByParentMenuId(int id)
        {
            try
            {
                return GetAllAsync(x=>x.Type == ResourceType.Menu && x.ParenMenuResourceId == id);
            }

            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

    }
    public interface IMenuResourceRepository : IBaseRepository<MenuResource, int>
    {
        Task<MenuResource> GetMenuResourceByMenuName(string menuName);
        Task<IEnumerable<MenuResource>> GetMenuByPageId(int pageId);
        Task<IEnumerable<MenuResource>> GetAllRootMenu();
        Task<IEnumerable<MenuResource>> GetAllRootMenuByParentMenuId(int id);

    }

}