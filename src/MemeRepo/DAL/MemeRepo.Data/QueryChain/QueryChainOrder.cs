namespace MemeRepo.Data.QueryChain
{
    internal class QueryChainOrder<T, TKey> where T : class
	{
		public bool IsAscending { get; set; }

		public Expression<Func<T, TKey>>? Selector { get; set; }
	}
}
