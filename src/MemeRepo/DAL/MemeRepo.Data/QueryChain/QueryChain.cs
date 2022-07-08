using LinqToDB;
using MemeRepo.Infrastructure.Data.QueryChain;

namespace MemeRepo.Data.QueryChain
{
    internal class QueryChain<T,F> : IQueryChain<T>, IOrderedQueryChain<T>
		where T : class
		where F : LinqToDB.Data.DataConnection
	{
		private readonly F _context;
		private readonly List<Expression<Func<T, object>>> _loadWithList;
		private readonly List<Expression<Func<T, bool>>> _whereList;
		private readonly List<QueryChainOrder<T, object>> _orderList;
		private Expression<Func<int>>? _skipExpr;
		private Expression<Func<int>>? _takeExpr;


		internal QueryChain(F context)
		{
			_loadWithList = new List<Expression<Func<T, object>>>();
			_whereList = new List<Expression<Func<T, bool>>>();
			_orderList = new List<QueryChainOrder<T, object>>();
			_context = context;
		}

		private IQueryable<T> ApplyLoadWith()
		{
			var query = _context.GetTable<T>().AsQueryable<T>();
			foreach (var expression in _loadWithList)
				query = query.LoadWith(expression);

			return query;
		}

		private IQueryable<T> Process()
		{
			var query = ApplyLoadWith();

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

		public IQueryChain<T> With(Expression<Func<T, object>> selector)
		{
			_loadWithList.Add(selector);
			return this;
		}

		public IFilteredQueryChain<T> Where(Expression<Func<T, bool>> selector)
		{
			_whereList.Add(selector);
			return this;
		}

		public IOrderedQueryChain<T> OrderBy(Expression<Func<T, object>> selector)
		{
			_orderList.Add(new QueryChainOrder<T, object> { IsAscending = true, Selector = selector });
			return this;
		}

		public IOrderedQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector)
		{
			_orderList.Add(new QueryChainOrder<T, object> { IsAscending = false, Selector = selector });
			return this;
		}

		public IOrderedQueryChain<T> ThenBy(Expression<Func<T, object>> selector)
		{
			_orderList.Add(new QueryChainOrder<T, object> { IsAscending = true, Selector = selector });
			return this;
		}

		public IOrderedQueryChain<T> ThenByDescending(Expression<Func<T, object>> selector)
		{
			_orderList.Add(new QueryChainOrder<T, object> { IsAscending = false, Selector = selector });
			return this;
		}

		IFilteredQueryChain<T> IFilteredQueryChain<T>.Skip(int count)
		{
			if (_skipExpr != null)
				throw new Exception("Skip rule already setted!");
			_skipExpr = () => count;
			return this;
		}

		IOrderedQueryChain<T> IOrderedQueryChain<T>.Skip(int count)
		{
			if (_skipExpr != null)
				throw new Exception("Skip rule already setted!");
			_skipExpr = () => count;
			return this;
		}

		IFilteredQueryChain<T> IFilteredQueryChain<T>.Skip(Expression<Func<int>> selector)
		{
			if (_skipExpr != null)
				throw new Exception("Skip rule already setted!");
			_skipExpr = selector;
			return this;
		}

		IOrderedQueryChain<T> IOrderedQueryChain<T>.Skip(Expression<Func<int>> selector)
		{
			if (_skipExpr != null)
				throw new Exception("Skip rule already setted!");
			_skipExpr = selector;
			return this;
		}

		IFilteredQueryChain<T> IFilteredQueryChain<T>.Take(int count)
		{
			if (_takeExpr != null)
				throw new Exception("Take rule already setted!");
			_takeExpr = () => count;
			return this;
		}

		IOrderedQueryChain<T> IOrderedQueryChain<T>.Take(int count)
		{
			if (_takeExpr != null)
				throw new Exception("Take rule already setted!");
			_takeExpr = () => count;
			return this;
		}

		IFilteredQueryChain<T> IFilteredQueryChain<T>.Take(Expression<Func<int>> selector)
		{
			if (_takeExpr != null)
				throw new Exception("Take rule already setted!");
			_takeExpr = selector;
			return this;
		}

		IOrderedQueryChain<T> IOrderedQueryChain<T>.Take(Expression<Func<int>> selector)
		{
			if (_takeExpr != null)
				throw new Exception("Take rule already setted!");
			_takeExpr = selector;
			return this;
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

		public async Task<List<TResult>> SelectToListAsync<TResult>(Expression<Func<T, TResult>> selector)
		{
			return await Process().Select(selector).ToListAsync();
		}

		public async Task<List<TResult>> SelectManyToListAsync<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector)
		{
			return await Process().SelectMany(selector).ToListAsync();
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
