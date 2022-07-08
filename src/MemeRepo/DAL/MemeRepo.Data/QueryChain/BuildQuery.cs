using MemeRepo.Infrastructure.Data.QueryChain;

namespace MemeRepo.Data.QueryChain
{
    public class BuildQuery<F> : IBuildQuery<F>
        where F : LinqToDB.Data.DataConnection
    {
        private readonly F _context;

        internal BuildQuery(F context)
        {
            _context = context;
        }

        /// <summary>
        /// Create query for specified type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public IQueryChain<T> For<T>() where T : class
        {
            return new QueryChain<T,F>(_context);
        }
    }
}
