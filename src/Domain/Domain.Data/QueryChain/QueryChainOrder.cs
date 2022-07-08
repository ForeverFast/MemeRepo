using System.Linq.Expressions;

namespace Domain.Data.QueryChain
{
    internal class QueryChainOrder<T, TKey>
    {
		public bool IsAscending { get; set; }

		public Expression<Func<T, TKey>>? Selector { get; set; }
	}
}
