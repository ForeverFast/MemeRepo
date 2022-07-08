using System.Linq.Expressions;

namespace Domain.Data.Interfaces.QueryChain
{
    public interface IQueryableChain<T> : IFilteredQueryChain<T>, IOrderedQueryChain<T>, ISelectableQueryChain<T>, IExecutableQueryChain<T> 
        where T : class
    {
        /// <summary>
		/// Specifies associations, that should be loaded for each loaded record from current table.
		/// All associations, specified in selector expression, will be loaded.
		/// Take into account that use of this method could require multiple queries to load all requested associations.
		/// </summary>
		/// <param name="selector">Association selection expression</param>
		IQueryableChain<T> With(Expression<Func<T, object>> selector);


        #region Sync

        /// <summary>
        /// Inserts record into table
        /// </summary>
        /// <param name="obj">Object of insert</param>
        /// <returns>Success insert flag</returns>
        public bool Insert(T obj);

        /// <summary>
        /// Inserts record into table
        /// </summary>
        /// <param name="obj">Object with data to insert.</param>
        /// <returns>Inserted record's identity value</returns>
        public object InsertWithIdentity(T obj);

        /// <summary>
        /// Inserts record into table
        /// </summary>
        /// <param name="obj">Object with data to insert.</param>
        /// <returns>Inserted record's identity int value</returns>
        public int InsertWithInt32Identity(T obj);

        /// <summary>
		/// Performs bulk insert operation.
		/// </summary>
		/// <param name="source">Records to insert.</param>
		/// <returns>Bulk insert operation status.</returns>
        public bool BulkInsert(IEnumerable<T> source);

        /// <summary>
        /// Updates record in table
        /// </summary>
        /// <param name="obj">Object with data to update.</param>
        /// <returns>Success update flag</returns>
        public bool Update(T obj);

        #endregion


        #region Async

        /// <summary>
        /// Inserts record asynchronously into table
        /// </summary>
        /// <param name="obj">Object of insert</param>
        /// <returns>Success insert flag</returns>
        public Task<bool> InsertAsync(T obj);

        /// <summary>
        /// Inserts record asynchronously into table
        /// </summary>
        /// <param name="obj">Object with data to insert.</param>
        /// <returns>Inserted record's identity value</returns>
        public Task<object> InsertWithIdentityAsync(T obj);

        /// <summary>
        /// Inserts record asynchronously into table
        /// </summary>
        /// <param name="obj">Object with data to insert.</param>
        /// <returns>Inserted record's identity int value</returns>
        public Task<int> InsertWithInt32IdentityAsync(T obj);

        /// <summary>
        /// Asynchronously updates record in table
        /// </summary>
        /// <param name="obj">Object with data to update.</param>
        /// <returns>Success update flag</returns>
        public Task<bool> UpdateAsync(T obj);

        /// <summary>
		/// Asynchronously performs bulk insert operation.
		/// </summary>
		/// <param name="source">Records to insert.</param>
        public Task<bool> BulkInsertAsync(IEnumerable<T> obj);

        public Task<bool> BulkUpdateAsync(IEnumerable<T> objs);

        #endregion
    }
}
