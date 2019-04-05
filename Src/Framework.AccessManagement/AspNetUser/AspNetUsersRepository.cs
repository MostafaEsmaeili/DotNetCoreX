using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Dapper;
using LinqToDB;
using Pishkhan.Domain.Api_Request_Response;
using Pishkhan.Domain.Entity;
using Pishkhan.Domain.Filter;
using Pishkhan.Framework.Repository;
using Pishkhan.Framework.Repository.Command;
using Pishkhan.Framework.Repository.Pagination;
using Pishkhan.Framework.UnitOfWork;
    
namespace Pishkhan.UserManagement.AspNetUser
{

    public class AspNetUsersRepository : Repository<ApplicationUser, int>, IAspNetUsersRepository
    {

        public AspNetUsersRepository(IDbFactory factory) : base(factory)
        {
        }

        public Task<ApplicationUser> GetUserByName(string userName)
        {
            try
            {
                return SelectFirstOrDefaultAsync(x => x.UserName == userName);
            }
            catch (Exception e)
            {
                Logger.ErrorException(e.Message, e);
                throw;
            }
        }


        public Task<IEnumerable<ApplicationRole>> GetAllRoleByUserName(string userName)
        {
            try
            {
                var command = new CustomCommand
                {
                    SqlCommand = "sec.GetAllRoleByUserName",
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };
                command.Parameters.Add("userName", userName, DbType.String);

                return GetAllAsync<ApplicationRole>(command);
            }
            catch (Exception e)
            {
                Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public Task<ApplicationRole> GetRoleByName(string roleName)
        {
            try
            {
                var command = new CustomCommand
                {
                    SqlCommand = "sec.GetRoleByName",
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };
                command.Parameters.Add("roleName", roleName, DbType.String);

                return GetAsync<ApplicationRole>(command);
            }
            catch (Exception e)
            {
                Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public Task<PageCollection<ApplicationUser>> GetAllUserByFilter(ApiRequest<GetAllUserFilter> filter)
        {
            try
            {

                var result = Query;
                if (!filter.Entity.Pharse.IsNullOrEmpty())
                {
                    result = result.Where(x => x.UserName.Contains(filter.Entity.Pharse))
                        .Where(x => x.Email.Contains(filter.Entity.Pharse));
                }

                result=result.SetOrderByExpression(filter);
                return GetPagedCollectionAsync(result, filter.OptionalFilter.Page,
                    filter.OptionalFilter.Take);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
    }

    public interface IAspNetUsersRepository : IRepository<ApplicationUser, int>
    {
        Task<ApplicationUser> GetUserByName(string userName);
        Task<IEnumerable<ApplicationRole>> GetAllRoleByUserName(string userName);
        Task<ApplicationRole> GetRoleByName(string roleName);
        Task<PageCollection<ApplicationUser>> GetAllUserByFilter(ApiRequest<GetAllUserFilter> filter);
    }
}
