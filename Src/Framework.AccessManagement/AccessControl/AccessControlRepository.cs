using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pishkhan.Common;
using Pishkhan.Domain.Api_Request_Response;
using Pishkhan.Domain.Entity;
using Pishkhan.Domain.Enum;
using Pishkhan.Domain.Filter;
using Pishkhan.Framework.Infra.IoC;
using Pishkhan.Framework.Infra.Logging;
using Pishkhan.Framework.Repository;
using Pishkhan.Framework.Repository.Command;
using Pishkhan.Framework.Repository.Pagination;
using Pishkhan.Framework.UnitOfWork;

namespace Pishkhan.UserManagement.AccessControl
{
public class AccessControlRepository : Repository<Domain.Entity.AccessControl, int>,IAccessControlRepository
    {

        public AccessControlRepository(IDbFactory factory) : base(factory)
        {
        }

        public async Task<bool> GetAccess(string userName, string roleName, int resourceId, int applicationId)
        {
            try
            {
                var cusomComman = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    SqlCommand = "sec.GetAccess",
                    Parameters = new DynamicParameters()
                };
                cusomComman.Parameters.Add("userName",userName,DbType.String);
                cusomComman.Parameters.Add("roleName",roleName,DbType.String);
                cusomComman.Parameters.Add("resourceId",resourceId,DbType.Int32);
                cusomComman.Parameters.Add("applicationId",applicationId,DbType.Int32);


                var res = await GetAsync<int>(cusomComman);
                return res>0;
            }
            catch (Exception e)
            {
               Logger.ErrorException(e.Message,e);
                throw;
            }
        }
        public Task AddAccess(string userName, string roleName, int resourceId,int applicationId,bool access , string createdBy)
        {
            try
            {
                var cusomComman = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    SqlCommand = "sec.AddAccess",
                    Parameters = new DynamicParameters()
                };
                cusomComman.Parameters.Add("userName",userName,DbType.String);
                cusomComman.Parameters.Add("roleName",roleName,DbType.String);
                cusomComman.Parameters.Add("resourceId",resourceId,DbType.Int32);
                cusomComman.Parameters.Add("applicationId",applicationId,DbType.Int32);
                cusomComman.Parameters.Add("access",access,DbType.Boolean);
                cusomComman.Parameters.Add("createdBy",createdBy,DbType.String);


               return UpdateAsync(cusomComman);
            }
            catch (Exception e)
            {
                Logger.ErrorException(e.Message,e);
                throw;
            }
        }

        public async Task<List<Domain.Dto.AccessControl>> GetAllPageAccessByUserName(string userName)
        {
            try
            {
                var page =await GetAllAccessByFilter(new BaseReportFilter<AccessControlFilter>
                {
                    OptionalFilter = new OptionalFilter
                    {
                        Page = 1,
                        Take = int.MaxValue,

                    },
                    ReportFilter = new AccessControlFilter
                    {
                        ResourceType = ResourceType.Page.GetId(),
                        UserName = userName
                    }
                });
               
                return page.ResultList.ToList();
            }
            catch (Exception e)
            {
               Logger.ErrorException(e.Message, e);
                throw;
            }
        }
        public async Task<List<Domain.Dto.AccessControl>> GetAllButtonAccessByUserName(string userName)
        {
            try
            {
                var page =await GetAllAccessByFilter(new BaseReportFilter<AccessControlFilter>
                {
                    OptionalFilter = new OptionalFilter
                    {
                        Page = 1,
                        Take = int.MaxValue,

                    },
                    ReportFilter = new AccessControlFilter
                    {
                        ResourceType = ResourceType.Button.GetId(),
                        UserName = userName
                    }
                });
               
                return page.ResultList.ToList();
            }
            catch (Exception e)
            {
                Logger.ErrorException(e.Message, e);
                throw;
            }
        }
        public Task<PageCollection<Domain.Dto.AccessControl>> GetAllAccessByFilter(BaseReportFilter<AccessControlFilter> request)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

              
                command.Parameters.Add("UserName", request.ReportFilter.UserName,DbType.String);
                command.Parameters.Add("RoleName", request.ReportFilter.RoleName,DbType.String);
                command.Parameters.Add("ResourceType", request.ReportFilter.ResourceType,DbType.Int32);
                command.Parameters.Add("pharse", request.ReportFilter.Pharse,DbType.String);

                command.Parameters.Add("pageSize", request.OptionalFilter.Take);
                command.Parameters.Add("skip", request.OptionalFilter.Page - 1);


                var orderClause = "";
                if (request.OptionalFilter?.Sort != null)
                    orderClause = request.OptionalFilter.Sort.Aggregate(orderClause,
                        (current, sort) => current + sort.Field + " " + sort.Dir + " ");
                command.Parameters.Add("orderClause", orderClause);
                command.SqlCommand = "sec.GetAllAccessByFilter";


                return  GetPageCollectionAsync<Domain.Dto.AccessControl>(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

    
    }

    public interface IAccessControlRepository : IRepository<Domain.Entity.AccessControl, int>
    {
        Task<bool> GetAccess(string userName, string roleName, int resourceId, int applicationId);
        Task<PageCollection<Domain.Dto.AccessControl>> GetAllAccessByFilter(BaseReportFilter<AccessControlFilter> request);
        Task AddAccess(string userName, string roleName, int resourceId, int applicationId, bool access , string createdBy);
        Task<List<Domain.Dto.AccessControl>> GetAllButtonAccessByUserName(string userName);
        Task<List<Domain.Dto.AccessControl>> GetAllPageAccessByUserName(string userName);

    }
}
