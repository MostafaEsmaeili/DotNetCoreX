//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;
//using Dapper;
//using Framework.AccessManagement.Entity;
//using Framework.Enum;
//using Framework.Repositories;

//namespace Framework.AccessManagement.Repository
//{
//    public class AccessControlRepository : LinqToDbRepositoryBase<AccessControl, int>, IAccessControlRepository
//    {

//        public async Task<bool> GetAccess(string userName, string roleName, int resourceId, int applicationId)
//        {
//            try
//            {
//                DbContext
//            }
//            catch (Exception e)
//            {
//                Logger.ErrorException(e.Message, e);
//                throw;
//            }
//        }
//        public Task AddAccess(string userName, string roleName, int resourceId, int applicationId, bool access, string createdBy)
//        {
//            try
//            {
//                var cusomComman = new CustomCommand
//                {
//                    CommandType = CommandType.StoredProcedure,
//                    SqlCommand = "sec.AddAccess",
//                    Parameters = new DynamicParameters()
//                };
//                cusomComman.Parameters.Add("userName", userName, DbType.String);
//                cusomComman.Parameters.Add("roleName", roleName, DbType.String);
//                cusomComman.Parameters.Add("resourceId", resourceId, DbType.Int32);
//                cusomComman.Parameters.Add("applicationId", applicationId, DbType.Int32);
//                cusomComman.Parameters.Add("access", access, DbType.Boolean);
//                cusomComman.Parameters.Add("createdBy", createdBy, DbType.String);


//                return UpdateAsync(cusomComman);
//            }
//            catch (Exception e)
//            {
//                Logger.ErrorException(e.Message, e);
//                throw;
//            }
//        }

//        public async Task<List<Domain.Dto.AccessControl>> GetAllPageAccessByUserName(string userName)
//        {
//            try
//            {
//                var page = await GetAllAccessByFilter(new BaseReportFilter<AccessControlFilter>
//                {
//                    OptionalFilter = new OptionalFilter
//                    {
//                        Page = 1,
//                        Take = int.MaxValue,

//                    },
//                    ReportFilter = new AccessControlFilter
//                    {
//                        ResourceType = ResourceType.Page.GetId(),
//                        UserName = userName
//                    }
//                });

//                return page.ResultList.ToList();
//            }
//            catch (Exception e)
//            {
//                Logger.ErrorException(e.Message, e);
//                throw;
//            }
//        }
//        public async Task<List<Domain.Dto.AccessControl>> GetAllButtonAccessByUserName(string userName)
//        {
//            try
//            {
//                var page = await GetAllAccessByFilter(new BaseReportFilter<AccessControlFilter>
//                {
//                    OptionalFilter = new OptionalFilter
//                    {
//                        Page = 1,
//                        Take = int.MaxValue,

//                    },
//                    ReportFilter = new AccessControlFilter
//                    {
//                        ResourceType = ResourceType.Button.GetId(),
//                        UserName = userName
//                    }
//                });

//                return page.ResultList.ToList();
//            }
//            catch (Exception e)
//            {
//                Logger.ErrorException(e.Message, e);
//                throw;
//            }
//        }
//        public Task<PageCollection<Domain.Dto.AccessControl>> GetAllAccessByFilter(BaseReportFilter<AccessControlFilter> request)
//        {
//            try
//            {
//                var command = new CustomCommand
//                {
//                    CommandType = CommandType.StoredProcedure,
//                    Parameters = new DynamicParameters()
//                };


//                command.Parameters.Add("UserName", request.ReportFilter.UserName, DbType.String);
//                command.Parameters.Add("RoleName", request.ReportFilter.RoleName, DbType.String);
//                command.Parameters.Add("ResourceType", request.ReportFilter.ResourceType, DbType.Int32);
//                command.Parameters.Add("pharse", request.ReportFilter.Pharse, DbType.String);

//                command.Parameters.Add("pageSize", request.OptionalFilter.Take);
//                command.Parameters.Add("skip", request.OptionalFilter.Page - 1);


//                var orderClause = "";
//                if (request.OptionalFilter?.Sort != null)
//                    orderClause = request.OptionalFilter.Sort.Aggregate(orderClause,
//                        (current, sort) => current + sort.Field + " " + sort.Dir + " ");
//                command.Parameters.Add("orderClause", orderClause);
//                command.SqlCommand = "sec.GetAllAccessByFilter";


//                return GetPageCollectionAsync<Domain.Dto.AccessControl>(command);
//            }
//            catch (Exception ex)
//            {
//                Logger.ErrorException(ex.Message, ex);
//                throw ex;
//            }
//        }


//    }

//    public interface IAccessControlRepository : IBaseRepository<AccessControl, int>
//    {
//        Task<bool> GetAccess(string userName, string roleName, int resourceId, int applicationId);
//        Task<PageCollection<Domain.Dto.AccessControl>> GetAllAccessByFilter(BaseReportFilter<AccessControlFilter> request);
//        Task AddAccess(string userName, string roleName, int resourceId, int applicationId, bool access, string createdBy);
//        Task<List<Domain.Dto.AccessControl>> GetAllButtonAccessByUserName(string userName);
//        Task<List<Domain.Dto.AccessControl>> GetAllPageAccessByUserName(string userName);

//    }
//}
