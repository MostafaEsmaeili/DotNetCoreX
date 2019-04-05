using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Dapper.AmbientContext;
using Dapper.FastCrud;
using Framework.Dapper.Repositories;
using Framework.Dependency;
using Framework.Entity;
using Framework.Exceptions;
using Framework.Logging;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LinqToDB;
using LinqToDB.Data;
using Sql = Dapper.FastCrud.Sql;

namespace Framework.Repositories
{
    public abstract class LinqToDbRepositoryBase<TEntity, TPrimaryKey> : IBaseRepository<TEntity,TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey> , new()
	where  TPrimaryKey : IComparable

    {
	    private readonly IAmbientDbContextLocator _ambientDbContextLocator;
        private readonly RepositoryContainer _container = RepositoryContainer.Instance;
        protected CustomLogger Logger => new CustomLogger(GetType().FullName);
        protected LinqToDbRepositoryBase(IAmbientDbContextLocator ambientDbContextLocator )
        {
	        _ambientDbContextLocator = ambientDbContextLocator;
        }
        protected TEntity CreateEntityAndSetKeyValue(TPrimaryKey key)
        {
	        var entity = CreateInstanceHelper.Resolve<TEntity>();
	        SetPrimaryKeyValue(entity, key);
	        return entity;
        }
        protected void SetPrimaryKeyValue(TEntity entity, TPrimaryKey value)
        {
	        if (_container.IsIEntity<TEntity, TPrimaryKey>())
	        {
	            if (entity is IEntity<TPrimaryKey> entityInterface)
		        {
			        entityInterface.Id = value;
			        return;
		        }
	        }
	        var primaryKeyPropertyInfo = GetPrimaryKeyPropertyInfo();
	        primaryKeyPropertyInfo.SetValue(entity, value);
        }
        private PropertyInfo GetPrimaryKeyPropertyInfo()
        {
	        var keys          = _container.GetKeys<TEntity>();
	        var primarKeyName = keys.FirstOrDefault(key => key.IsPrimaryKey)?.PropertyName;
	        var properies     = _container.GetProperties<TEntity>(keys);
	        if (keys == null || primarKeyName == null || properies == null)
	        {
		        throw new NoPkException(
		                                "There is no primary ket for this entity, please create your logic or add a key attribute to the entity");
	        }
	        var primarKeyValue =
		        properies.FirstOrDefault(property => property.Name.Equals(primarKeyName, StringComparison.Ordinal));
	        return primarKeyValue;
        }

        public virtual IDbConnection Connection
        {
	        get
	        {
		        var context = _ambientDbContextLocator.GetConnectionDetail();
              
		        if (context?.Connection == null)
			      throw new InvalidOperationException("Connection is null or is closed.");
		        return context.Connection;
	        }
        }


        public virtual DataConnection DbContext 
        {
            get
            {
                var context = _ambientDbContextLocator.GetConnectionDetail();
              
                if (context?.DataConnection == null)
                    throw new InvalidOperationException("Connection is null or is closed.");
                return context.DataConnection;
            }
        }

        /// <summary>
        ///     Gets the active transaction. If Dapper is active then <see>
        ///         <cref>IUnitOfWork</cref>
        ///     </see>
        ///     should be started before
        ///     and there must be an active transaction.
        /// </summary>
        /// <value>
        ///     The active transaction.
        /// </value>
        public virtual IDbTransaction ActiveTransaction => _ambientDbContextLocator.GetConnectionDetail()?.Transaction;
       

        public virtual TEntity Single(TPrimaryKey id)
        {
	        if (_container.IsIEntity<TEntity, TPrimaryKey>())
	        {
		        return Connection.QuerySingleOrDefault<TEntity>(
		                                                     $"SELECT * FROM {Sql.Table<TEntity>()} WHERE Id = @Id",
		                                                     new {Id = id});
	        }
	        var entity = CreateEntityAndSetKeyValue(id);
	        return Connection.Get(entity,x=>x.AttachToTransaction(ActiveTransaction));
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {

	       
		        var query = DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
            return query.FirstOrDefault();

        }

        protected virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {

	      
		        var query = DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);

		        return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> SingleAsync(TPrimaryKey id)
        {

	        if (_container.IsIEntity<TEntity, TPrimaryKey>())
	        {
		        return await Connection.QuerySingleOrDefaultAsync(
		                                                        $"SELECT * FROM {Sql.Table<TEntity>()} WHERE Id = @Id",
		                                                        new {Id = id},transaction:ActiveTransaction);
	        }
	        var entity = CreateEntityAndSetKeyValue(id);
            
	        return await Connection.GetAsync(entity, x=>x.AttachToTransaction(ActiveTransaction));
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
	        return Single(id);
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
	        return await SingleAsync(id);
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
	        return Single(id);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
	        return  await SingleAsync(id);
        }

        protected virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
	        return await SingleAsync(predicate);
        }

        protected virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
	        return Single(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
	     
		        return DbContext.GetTable<TEntity>().ToList();
	        
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
	      
		        return await DbContext.GetTable<TEntity>().ToListAsync();
	        
        }

        protected IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
	       
		        var query = DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
		        return  query.ToList();
        }

        protected async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
	       
		        var query = DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
		        return await query.ToListAsync();
        }

        public async Task<IPagedResult<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, IEnumerable<PageSortRequest> pageRequests)
        {
            var query=   DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
            query = query.SetOrderByExpression(pageRequests);
            var total = DynamicQueryableExtensions.Count(query);
            query = query.Page(pageNumber, itemsPerPage);
            var result = new PagedResultDto<TEntity>
            {
               Items  = await  query.ToListAsync(),
                TotalCount = total
            };
            return await Task.FromResult(result);
        }

        public IPagedResult<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, IEnumerable<PageSortRequest> pageRequests)
        {
            var query=   DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
            query = query.SetOrderByExpression(pageRequests);
            var total = DynamicQueryableExtensions.Count(query);
            query = query.Page(pageNumber, itemsPerPage);
            var result = new PagedResultDto<TEntity>
            {
                Items  =   query.ToList(),
                TotalCount = total
            };
            return result;
        }


        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
	        
		        var query = DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
		        return  query.Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
	      
		        var query = DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate);
		        return await query.CountAsync();
        }


        public void Insert(TEntity entity)
        {
            DbContext.Insert(entity);
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return (TPrimaryKey) DbContext.InsertWithIdentity(entity);
        }

        public Task InsertAsync(TEntity entity)
        {
           return DbContext.InsertAsync(entity);
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return (TPrimaryKey) await DbContext.InsertWithIdentityAsync(entity);
        }

        public int Update(TEntity entity)
        {
           return DbContext.Update(entity);
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
           return DbContext.UpdateAsync(entity);

        }

        public int Delete(TEntity entity)
        {
          return  DbContext.Delete(entity);
        }

        protected int Delete(Expression<Func<TEntity, bool>> predicate)
        {
           return DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate).Delete();
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            return  DbContext.DeleteAsync(entity);
        }

        public  Task<DataSourceResult> GetAllPagedAsync(DataSourceRequest request,
            params KeyValuePair<string, string>[] replaceSortFields)
        {
            var newListOfSort = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                newListOfSort = request.Sorts.Select(x => new SortDescriptor
                {
                    SortDirection = x.SortDirection,
                    SortCompare = x.SortCompare,
                    Member = ReplaceSortMember(x.Member.Split('.')[x.Member.Split('.').Length - 1], replaceSortFields)
                }).ToList();

            }
            request.Sorts = newListOfSort;
            var query =  DbContext.GetTable<TEntity>().ToDataSourceResultAsync(request);
            return query;
        }

        /// <summary>
        /// this method replace sort field string with replacement value
        /// </summary>
        /// <param name="member"></param>
        /// <param name="replaceSortFields"></param>
        /// <returns></returns>
        private string ReplaceSortMember(string member, KeyValuePair<string, string>[] replaceSortFields)
        {
            var list = replaceSortFields.ToList();
            return list.Any(x => x.Key == member) ? list.FirstOrDefault(x => x.Key == member).Value : member;
        }
        public DataSourceResult GetAllPaged(DataSourceRequest request, params KeyValuePair<string, string>[] replaceSortFields)
        {
            var newListOfSort = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                newListOfSort = request.Sorts.Select(x => new SortDescriptor
                {
                    SortDirection = x.SortDirection,
                    SortCompare = x.SortCompare,
                    Member = ReplaceSortMember(x.Member.Split('.')[x.Member.Split('.').Length - 1], replaceSortFields)
                }).ToList();
            }
            request.Sorts = newListOfSort;
            var query =  DbContext.GetTable<TEntity>().ToDataSourceResult(request);
            return query;
        }

        public Task<DataSourceResult> GetAllPagedAsync<TAny>(DataSourceRequest request,IQueryable<TAny> queryable, params KeyValuePair<string, string>[] replaceSortFields) where TAny : class, new()
        {
            var newListOfSort = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                newListOfSort = request.Sorts.Select(x => new SortDescriptor
                {
                    SortDirection = x.SortDirection,
                    SortCompare = x.SortCompare,
                    Member = ReplaceSortMember(x.Member.Split('.')[x.Member.Split('.').Length - 1], replaceSortFields)
                }).ToList();

            }
            request.Sorts = newListOfSort;
            var query =  queryable.ToDataSourceResultAsync(request);
            return query;
        }

        public DataSourceResult GetAllPaged<TAny>(DataSourceRequest request, IQueryable<TAny> queryable,params KeyValuePair<string, string>[] replaceSortFields) where TAny : class, new()
        {
            var newListOfSort = new List<SortDescriptor>();
            if (request.Sorts != null)
            {
                newListOfSort = request.Sorts.Select(x => new SortDescriptor
                {
                    SortDirection = x.SortDirection,
                    SortCompare = x.SortCompare,
                    Member = ReplaceSortMember(x.Member.Split('.')[x.Member.Split('.').Length - 1], replaceSortFields)
                }).ToList();
            }
            request.Sorts = newListOfSort;
            var query =  queryable.ToDataSourceResult(request);
            return query;
        }


        protected Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.GetTable<TEntity>().CreateQuery<TEntity>(predicate).DeleteAsync();
        }
    }
    
}

