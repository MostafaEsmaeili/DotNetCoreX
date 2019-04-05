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

namespace Pishkhan.UserManagement.MenuResources
{
     public class MenuResourceRepository : Repository<MenuResource, int>, IMenuResourceRepository
    {
       

        public MenuResourceRepository(IDbFactory factory) : base(factory)
        {
        }



        public Task<PageCollection<MenuResource>> GetAllMenuResourcesByFilter(
            BaseReportFilter<MenuRequestFilter> request)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

              
                command.Parameters.Add("MenuTitle", request.ReportFilter.MenuTitle,DbType.String);
                command.Parameters.Add("MenuName", request.ReportFilter.MenuName,DbType.String);

                command.Parameters.Add("pageSize", request.OptionalFilter.Take);
                command.Parameters.Add("skip", request.OptionalFilter.Page - 1);


                var orderClause = "";
                if (request.OptionalFilter?.Sort != null)
                    orderClause = request.OptionalFilter.Sort.Aggregate(orderClause,
                        (current, sort) => current + sort.Field + " " + sort.Dir + " ");
                command.Parameters.Add("orderClause", orderClause);
                command.SqlCommand = "sec.GetAllMenuResourcesByFilter";

           

                 return  GetPageCollectionAsync<MenuResource>(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public Task<MenuResource> GetMenuResourceyByMenuName(string menuName)
        {
            try
            {

                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

                command.Parameters.Add("menuName", menuName, DbType.String);
                command.SqlCommand = "sec.GetMenuResourceyByMenuName";


                return GetAsync(command);
            }


            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<IEnumerable<MenuResource>> GetMenuByPageId(int pageId)
        {
            try
            {

                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

                command.Parameters.Add("pageId", pageId, DbType.Int32);
                command.SqlCommand = "sec.GetMenuByPageId";


                return GetAllAsync<MenuResource>(command);
            }


            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<IEnumerable<MenuResource>> GetAllRootMenu()
        {
            try
            {

                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters(),
                    SqlCommand = "sec.GetAllRootMenu"
                };


                return GetAllAsync<MenuResource>(command);
            }


            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public Task<IEnumerable<MenuResource>> GetAllRootMenuByParentMenuId(int id)
        {
            try
            {

                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters(),
                    SqlCommand = "sec.GetAllRootMenuByParentMenuId"
                };

                command.Parameters.Add("id",id,DbType.Int32);


                return GetAllAsync<MenuResource>(command);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    public interface IMenuResourceRepository : IRepository<MenuResource, int>
    {
        Task<PageCollection<MenuResource>> GetAllMenuResourcesByFilter(BaseReportFilter<MenuRequestFilter> request);
        Task<MenuResource> GetMenuResourceyByMenuName(string menuName);
        Task<IEnumerable<MenuResource>> GetMenuByPageId(int pageId);

        Task<IEnumerable<MenuResource>> GetAllRootMenu();
        Task<IEnumerable<MenuResource>> GetAllRootMenuByParentMenuId(int id);


    }

}