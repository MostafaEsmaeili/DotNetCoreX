using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pishkhan.Domain.Api_Request_Response;
using Pishkhan.Domain.Filter;
using Pishkhan.Framework.Infra.IoC;
using Pishkhan.Framework.Repository;
using Pishkhan.Framework.Repository.Command;
using Pishkhan.Framework.Repository.Pagination;
using Pishkhan.Framework.UnitOfWork;

namespace Pishkhan.UserManagement.PageResource
{
    public class PageResourceRepository : Repository<Domain.Entity.PageResource,int> , IPageResourceRepository
    {
        public PageResourceRepository(IDbFactory factory) : base(factory)
        {
        }

        public Task<PageCollection<Domain.Entity.PageResource>> GetAllPageResourceByFilter(
            BaseReportFilter<PageRequestFilter> request)
        {
            try
            {
                var command = new CustomCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters = new DynamicParameters()
                };

              
                command.Parameters.Add("PageTitle", request.ReportFilter.PageTitle,DbType.String);
                command.Parameters.Add("PageName", request.ReportFilter.PageName,DbType.String);

                command.Parameters.Add("pageSize", request.OptionalFilter.Take);
                command.Parameters.Add("skip", request.OptionalFilter.Page - 1);


                var orderClause = "";
                if (request.OptionalFilter?.Sort != null)
                    orderClause = request.OptionalFilter.Sort.Aggregate(orderClause,
                        (current, sort) => current + sort.Field + " " + sort.Dir + " ");
                command.Parameters.Add("orderClause", orderClause);
                command.SqlCommand = "sec.GetAllPageResourceByFilter";


                return  GetPageCollectionAsync<Domain.Entity.PageResource>(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public Task<Domain.Entity.PageResource> GetPageRepositoryByPageName(string pageName)
        {
            try
            {
                try
                {
                    var command = new CustomCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        Parameters = new DynamicParameters()
                    };
              
                    command.Parameters.Add("PageName", pageName,DbType.String);
                    command.SqlCommand = "sec.GetPageRepositoryByPageName";

        

                    return  GetAsync<Domain.Entity.PageResource>(command);
                }
                catch (Exception ex)
                {
                    Logger.ErrorException(ex.Message, ex);
                    throw ex;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private DataTable ToDataTable(List<int> apiIds)
        {
            try
            {
                var table = new DataTable();
                table.Columns.Add("ApiId", typeof(int));
               

                foreach (var isin in apiIds)
                {
                    try
                    {
                        var row = table.NewRow();
                        row["ApiId"] = isin;
                        table.Rows.Add(row);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public interface IPageResourceRepository : IRepository<Domain.Entity.PageResource, int>
    {
        Task<PageCollection<Domain.Entity.PageResource>> GetAllPageResourceByFilter(
            BaseReportFilter<PageRequestFilter> request);
        Task<Domain.Entity.PageResource> GetPageRepositoryByPageName(string pageName);
    }
}