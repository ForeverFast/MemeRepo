using Domain.Data.Interfaces.QueryChain;

namespace Domain.Data.QueryChain
{
    /// <summary>
    /// Helper for building queries to database
    /// </summary>
    public class BuildQuery<F> where F : DataConnection
    {
        private readonly F _context;

        public BuildQuery(F context)
        {
            _context = context;
        }

        /// <summary>
        /// Create query for specified type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public IQueryableChain<T> For<T>() where T : class
        {
			return new QueryableChain<T,F>(_context);
		}
	}
}
