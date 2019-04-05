//using System;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using Dapper;
//using Pishkhan.Domain.Api_Request_Response;
//using Pishkhan.Domain.Entity;
//using Pishkhan.Domain.Filter;
//using Pishkhan.Framework.Repository;
//using Pishkhan.Framework.Repository.Command;
//using Pishkhan.Framework.Repository.Pagination;
//using Pishkhan.Framework.UnitOfWork;

//namespace Pishkhan.UserManagement.GridResource
//{
//    public class GridActionResourceRepository : Repository<GridActionResource, int> , IGridActionResourceRepository
//    {
//        public GridActionResourceRepository(IDbFactory factory) : base(factory)
//        {
//        }
//        public Task<PageCollection<GridActionResource>> GetAllGridActionResourcesByFilter(ApiRequest<GridResourceFilter> request)
//        {
//            try
//            {
//                var command = new CustomCommand
//                {
//                    CommandType = CommandType.StoredProcedure,
//                    Parameters = new DynamicParameters()
//                };

              
//                command.Parameters.Add("GridName", request.Entity.GridName,DbType.String);
//                command.Parameters.Add("ActionName", request.Entity.ActionName,DbType.String);
//                command.Parameters.Add("GridActionType", request.Entity.GridActionType,DbType.Int32);


//                command.Parameters.Add("pageSize", request.OptionalFilter.take);
//                command.Parameters.Add("skip", request.OptionalFilter.page - 1);


//                var orderClause = "";
//                if (request.OptionalFilter?.sort != null)
//                    orderClause = request.OptionalFilter.sort.Aggregate(orderClause,
//                        (current, sort) => current + sort.field + " " + sort.dir + " ");
//                command.Parameters.Add("orderClause", orderClause);
//                command.SqlCommand = "dbo.GetAllGridActionResourcesByFilter";

            
//                return  GetPageCollectionAsync<GridActionResource>(command);
//            }
//            catch (Exception ex)
//            {
//                Logger.ErrorException(ex.Message, ex);
//                throw ex;
//            }
//        }
//    }
//    public interface IGridActionResourceRepository  : IRepository<GridActionResource , int>
//    {
        
//    }
//}