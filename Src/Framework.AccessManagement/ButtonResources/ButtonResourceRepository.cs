using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pishkhan.Domain.Api_Request_Response;
using Pishkhan.Domain.Entity;
using Pishkhan.Domain.Filter;
using Pishkhan.Framework.Infra.IoC;
using Pishkhan.Framework.Infra.Logging;
using Pishkhan.Framework.Repository;
using Pishkhan.Framework.Repository.Command;
using Pishkhan.Framework.Repository.Pagination;
using Pishkhan.Framework.UnitOfWork;

namespace Pishkhan.UserManagement.ButtonResources
{
    public class ButtonResourceRepository : Repository<ButtonResource, int>, IButtonResourceRepository
    {

        public ButtonResourceRepository(IDbFactory factory) : base(factory)
        {
        }
        public Task<PageCollection<Domain.Dto.ButtonResource>> GetAllButtunResourcesByFilter(BaseReportFilter<ButtonResourceFilter> request)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

              
                command.Parameters.Add("ButtonTitle", request.ReportFilter.ButtonTitle,DbType.String);
                command.Parameters.Add("ButtonName", request.ReportFilter.ButtonName,DbType.String);

                command.Parameters.Add("pageSize", request.OptionalFilter.Take);
                command.Parameters.Add("skip", request.OptionalFilter.Page - 1);


                var orderClause = "";
                if (request.OptionalFilter?.Sort != null)
                    orderClause = request.OptionalFilter.Sort.Aggregate(orderClause,
                        (current, sort) => current + sort.Field + " " + sort.Dir + " ");
                command.Parameters.Add("orderClause", orderClause);
                command.SqlCommand = "sec.GetAllButtunResourcesByFilter";

            
                return  GetPageCollectionAsync<Domain.Dto.ButtonResource>(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public Task<ButtonResource> GetButtonResourceByButtonNameAndPageName(string buttonName, int pageId)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

              
                command.Parameters.Add("buttonName", buttonName,DbType.String);
                command.Parameters.Add("pageId", pageId,DbType.Int32);

                command.SqlCommand = "sec.GetButtonResourceByButtonNameAndPageName";
            
                return  GetAsync<ButtonResource>(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
        public Task<ButtonResource> GetButtonResourceByButtonIdAndPageId(int buttonId, int pageId)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };
              
                command.Parameters.Add("buttonId", buttonId,DbType.Int32);
                command.Parameters.Add("pageId", pageId,DbType.Int32);

                command.SqlCommand = "sec.GetButtonResourceByButtonIdAndPageId";

                return GetAsync(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
        public Task<IEnumerable<ButtonResource>> GetButtonByPageId(int pageId)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

              
                command.Parameters.Add("pageId", pageId,DbType.Int32);

                command.SqlCommand = "sec.GetButtonByPageId";
            
                return  GetAllAsync<ButtonResource>(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
    }
    public interface IButtonResourceRepository : IRepository<ButtonResource, int>
    {
        Task<PageCollection<Domain.Dto.ButtonResource>> GetAllButtunResourcesByFilter(BaseReportFilter<ButtonResourceFilter> request);
        Task<ButtonResource> GetButtonResourceByButtonNameAndPageName(string buttonName, int pageId);
        Task<IEnumerable<ButtonResource>> GetButtonByPageId(int pageId);
        Task<ButtonResource> GetButtonResourceByButtonIdAndPageId(int buttonId, int pageId);
    }
}
