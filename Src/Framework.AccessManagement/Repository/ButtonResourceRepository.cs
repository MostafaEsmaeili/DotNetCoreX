using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.AmbientContext;
using Framework.AccessManagement.Entity;
using Framework.Enum;
using Framework.Repositories;

namespace Framework.AccessManagement.Repository
{
    public class ElementResourceRepository : LinqToDbRepositoryBase<ElementResource, int>, IElementResourceRepository
    {
        public ElementResourceRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }
        public Task<ElementResource> GetElementResourceByButtonNameAndPageName(string elementName, int pageId)
        {
            try
            {
                return FirstOrDefaultAsync(x =>
                    x.Type == ResourceType.Element && x.Name == elementName && x.PageResourceId == pageId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
        public Task<ElementResource> GetElementResourceByElementIdAndPageId(int elementId, int pageId)
        {
            try
            {
                return FirstOrDefaultAsync(x=>x.Type == ResourceType.Element && x.Id==elementId && x.PageResourceId==pageId );
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
        public Task<IEnumerable<ElementResource>> GetElementByPageId(int pageId)
        {
            try
            {
              
                return  GetAllAsync(x=>x.Type == ResourceType.Element && x.PageResourceId == pageId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

      
    }
    public interface IElementResourceRepository : IBaseRepository<ElementResource, int>
    {
        Task<ElementResource> GetElementResourceByButtonNameAndPageName(string elementName, int pageId);
        Task<IEnumerable<ElementResource>> GetElementByPageId(int pageId);
        Task<ElementResource> GetElementResourceByElementIdAndPageId(int elementId, int pageId);
    }
}
