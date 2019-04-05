using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Framework.Entity;
using JetBrains.Annotations;
using Kendo.Mvc.UI;

namespace Framework.Dapper.Repositories
{
    /// <inheritdoc />
    /// <summary>
    ///     Dapper repository abstraction interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="T:Framework.Repositories.IBaseRepository" />
    public interface IBaseRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IComparable
    {
        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CanBeNull]
        TEntity FirstOrDefault([NotNull] TPrimaryKey id);

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id);





        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAll();

        /// <summary>
        ///     Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync();




        /// <summary>
        ///     Gets the list paged asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="pageRequests">Sort direction items</param>
        /// <returns></returns>
        [NotNull]
        Task<IPagedResult<TEntity>> GetAllPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate,
            int pageNumber, int itemsPerPage, [NotNull] IEnumerable<PageSortRequest> pageRequests);

        /// <summary>
        ///     Gets the list paged asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="pageRequests">Sort direction items</param>
        /// <returns></returns>
        [NotNull]
        IPagedResult<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber,
            int itemsPerPage, [NotNull] IEnumerable<PageSortRequest> pageRequests);


        /// <summary>
        ///     Counts the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        int Count([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Counts the asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        [NotNull]
        Task<int> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate);



        /// <summary>
        ///     Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert([NotNull] TEntity entity);

        /// <summary>
        ///     Inserts the and get identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        TPrimaryKey InsertAndGetId([NotNull] TEntity entity);

        /// <summary>
        ///     Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task InsertAsync([NotNull] TEntity entity);

        /// <summary>
        ///     Inserts the and get identifier asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity);

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        int Update([NotNull] TEntity entity);

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        [NotNull]
        Task<int> UpdateAsync([NotNull] TEntity entity);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        int Delete([NotNull] TEntity entity);


        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [NotNull]
        Task<int> DeleteAsync([NotNull] TEntity entity);


        /// <summary>
        ///     Gets the list paged asynchronous.
        /// </summary>
        /// <param name="request">Kendo DataSource Request.</param>
        /// <param name="replaceSortFields">Some time it needed to replace some sort criteria by another, for example client sent GenderTitle and it should Replace with Gender.
        /// </param>
        /// <returns></returns>
        [NotNull]
        Task<DataSourceResult> GetAllPagedAsync([NotNull] DataSourceRequest request,
            params KeyValuePair<string, string>[] replaceSortFields);

        /// <summary>
        ///     Gets the list paged asynchronous.
        /// </summary>
        /// <param name="request">Kendo DataSource Request.</param>
        /// <param name="replaceSortFields">Some time it needed to replace some sort criteria by another, for example client sent GenderTitle and it should Replace with Gender.
        /// </param>
        /// <returns></returns>
        [NotNull]
        DataSourceResult GetAllPaged([NotNull] DataSourceRequest request,
            params KeyValuePair<string, string>[] replaceSortFields);


        /// <summary>
        ///     Gets the list paged asynchronous.
        /// </summary>
        /// <param name="request">Kendo DataSource Request.</param>
        /// <param name="queryable"></param>
        /// <param name="replaceSortFields">Some time it needed to replace some sort criteria by another, for example client sent GenderTitle and it should Replace with Gender.
        /// </param>
        /// <returns></returns>
        [NotNull]
        Task<DataSourceResult> GetAllPagedAsync<TAny>([NotNull] DataSourceRequest request, IQueryable<TAny> queryable,
            params KeyValuePair<string, string>[] replaceSortFields) where TAny : class, new ();

        /// <summary>
        ///     Gets the list paged asynchronous.
        /// </summary>
        /// <param name="request">Kendo DataSource Request.</param>
        /// <param name="queryable"></param>
        /// <param name="replaceSortFields">Some time it needed to replace some sort criteria by another, for example client sent GenderTitle and it should Replace with Gender.
        /// </param>
        /// <returns></returns>
        [NotNull]
        DataSourceResult GetAllPaged<TAny>([NotNull] DataSourceRequest request,IQueryable<TAny> queryable,
            params KeyValuePair<string, string>[] replaceSortFields) where TAny : class, new ();
    }
    
}
