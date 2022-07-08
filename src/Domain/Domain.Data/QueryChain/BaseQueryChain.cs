using Domain.Data.Interfaces.QueryChain;
using LinqToDB;
using System.Linq.Expressions;

namespace Domain.Data.QueryChain
{
    internal abstract class BaseQueryChain<T, F> 
        : IFilteredQueryChain<T>, IOrderedQueryChain<T>, ISelectableQueryChain<T>, IExecutableQueryChain<T>
        where T : class
        where F : DataConnection
    {
        protected readonly F _context;
        protected readonly List<Expression<Func<T, bool>>> _whereList;
        protected readonly List<QueryChainOrder<T, object>> _orderList;
        protected Expression<Func<int>>? _skipExpr;
        protected Expression<Func<int>>? _takeExpr;
        private IQueryable<T> _prevQuery;

        internal BaseQueryChain(F context, IQueryable<T> query)
        {
            _whereList = new List<Expression<Func<T, bool>>>();
            _orderList = new List<QueryChainOrder<T, object>>();
            _orderList = new List<QueryChainOrder<T, object>>();
            _context = context;
            _prevQuery = query;
        }

        protected virtual IQueryChain<T> GetQueryChain() => new QueryChain<T,F>(_context, _prevQuery);

        protected virtual IQueryable<T> Process()
        {
            return PrepareQuery(_prevQuery);
        }

        protected IQueryable<T> PrepareQuery(IQueryable<T> query)
        {
            foreach (var expression in _whereList)
            {
                query = query.Where(expression);
            }

            if (_orderList.Count > 0)
            {
                IOrderedQueryable<T>? ordered = null;
                foreach (var queryChainOrder in _orderList)
                {
                    if (queryChainOrder.Selector is not null)
                    {
                        if (ordered is null)
                            ordered = queryChainOrder!.IsAscending
                                ? query.OrderBy(queryChainOrder!.Selector)
                                : query.OrderByDescending(queryChainOrder!.Selector);
                        else
                            ordered = queryChainOrder.IsAscending
                                ? ordered.ThenBy(queryChainOrder!.Selector)
                                : ordered.ThenByDescending(queryChainOrder!.Selector);
                    }
                }
                if (ordered is not null)
                    query = ordered;
            }

            if (_skipExpr is not null)
                query = query.Skip(_skipExpr);

            if (_takeExpr is not null)
                query = query.Take(_takeExpr);

            return query;
        }

        public IQueryChain<T> Where(Expression<Func<T, bool>> selector)
        {
            _whereList.Add(selector);
            return GetQueryChain();
        }

        public IQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> selector)
        {
            if (condition)
                _whereList.Add(selector);

            return GetQueryChain();
        }

        public IQueryChain<T> OrderBy(Expression<Func<T, object>> selector)
        {
            _orderList.Add(new QueryChainOrder<T, object> { IsAscending = true, Selector = selector });
            return GetQueryChain();
        }

        public IQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector)
        {
            _orderList.Add(new QueryChainOrder<T, object> { IsAscending = false, Selector = selector });
            return GetQueryChain();
        }

        public IQueryChain<T> ThenBy(Expression<Func<T, object>> selector)
        {
            _orderList.Add(new QueryChainOrder<T, object> { IsAscending = true, Selector = selector });
            return GetQueryChain();
        }

        public IQueryChain<T> ThenByDescending(Expression<Func<T, object>> selector)
        {
            _orderList.Add(new QueryChainOrder<T, object> { IsAscending = false, Selector = selector });
            return GetQueryChain();
        }

        IQueryChain<T> IFilteredQueryChain<T>.Skip(int count)
        {
            if (_skipExpr != null)
                throw new Exception("Skip rule already setted!");
            _skipExpr = () => count;
            return GetQueryChain();
        }

        IQueryChain<T> IFilteredQueryChain<T>.Skip(Expression<Func<int>> selector)
        {
            if (_skipExpr != null)
                throw new Exception("Skip rule already setted!");
            _skipExpr = selector;
            return GetQueryChain();
        }

        IQueryChain<T> IFilteredQueryChain<T>.Take(int count)
        {
            if (_takeExpr != null)
                throw new Exception("Take rule already setted!");
            _takeExpr = () => count;
            return GetQueryChain();
        }

        IQueryChain<T> IFilteredQueryChain<T>.Take(Expression<Func<int>> selector)
        {
            if (_takeExpr != null)
                throw new Exception("Take rule already setted!");
            _takeExpr = selector;
            return GetQueryChain();
        }

        public IQueryChain<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class
        {
            var query = Process().Select(selector);
            return new QueryChain<TResult, F>(_context, query);
        }

        public IQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector) where TResult : class
        {
            var query = Process().SelectMany(selector);
            return new QueryChain<TResult, F>(_context, query);
        }

        #region Sync

        public T? FirstOrDefault()
        {
            return Process().FirstOrDefault();
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Process().FirstOrDefault(predicate);
        }

        public T First()
        {
            return Process().First();
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return Process().First(predicate);
        }

        public T? SingleOrDefault()
        {
            return Process().SingleOrDefault();
        }

        public T? SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Process().SingleOrDefault(predicate);
        }

        public T? LastOrDefault()
        {
            return Process().LastOrDefault();
        }

        public T? LastOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Process().LastOrDefault(predicate);
        }

        public List<T> ToList()
        {
            return Process().ToList();
        }

        public List<TResult> SelectToList<TResult>(Expression<Func<T, TResult>> selector)
        {
            return Process().Select(selector).ToList();
        }

        public List<TResult> SelectManyToList<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector)
        {
            return Process().SelectMany(selector).ToList();
        }

        public int Count()
        {
            return Process().Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return Process().Count(predicate);
        }

        public bool Any()
        {
            return Process().Any();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return Process().Any(predicate);
        }

        public bool All(Expression<Func<T, bool>> predicate)
        {
            return Process().All(predicate);
        }

        #endregion

        #region Async

        public async Task<T?> FirstOrDefaultAsync()
        {
            return await Process().FirstOrDefaultAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Process().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> FirstAsync()
        {
            return await Process().FirstAsync();
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await Process().FirstAsync(predicate);
        }

        public async Task<T?> SingleOrDefaultAsync()
        {
            return await Process().SingleOrDefaultAsync();
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Process().SingleOrDefaultAsync(predicate);
        }

        public async Task<List<T>> ToListAsync()
        {
            return await Process().ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Process().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Process().CountAsync(predicate);
        }

        public async Task<bool> AnyAsync()
        {
            return await Process().AnyAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Process().AnyAsync(predicate);
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Process().AllAsync(predicate);
        }

        #endregion
    }
}
