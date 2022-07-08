using System.Linq.Expressions;

namespace Domain.Data.Interfaces.QueryChain
{
    public interface IFilteredQueryChain<T>
    {
		/// <summary>
		/// Filters a sequence of values based on a predicate
		/// </summary>
		/// <param name="selector">A function to test each element for a condition</param>
		IQueryChain<T> Where(Expression<Func<T, bool>> selector);

        /// <summary>
		/// Filters a sequence of values based on a predicate if a condition is true
		/// </summary>
		/// <param name="selector">A function to test each element for a condition</param>
		/// <param name="condition">filtration conditions</param>
		IQueryChain<T> WhereIf(bool condition, Expression<Func<T, bool>> selector);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key
        /// </summary>
        /// <param name="selector">A function to extract a key from an element</param>
        IQueryChain<T> OrderBy(Expression<Func<T, object>> selector);

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key
        /// </summary>
        /// <param name="selector">A function to extract a key from an element</param>
        IQueryChain<T> OrderByDescending(Expression<Func<T, object>> selector);

		/// <summary>
		/// Bypasses a specified number of elements in a sequence and then returns the remaining elements
		/// </summary>
		/// <param name="count">The number of elements to skip before returning the remaining elements</param>
		IQueryChain<T> Skip(int count);

		/// <summary>
		/// Bypasses a specified number of elements in a sequence and then returns the remaining elements
		/// </summary>
		/// <param name="selector">A function to specify the number of elements to skip</param>
		IQueryChain<T> Skip(Expression<Func<int>> selector);

		/// <summary>
		/// Returns a specified number of contiguous elements from the start of a sequence
		/// </summary>
		/// <param name="count">The number of elements to return</param>
		IQueryChain<T> Take(int count);

		/// <summary>
		/// Returns a specified number of contiguous elements from the start of a sequence
		/// </summary>
		/// <param name="selector">A function to specify the number of elements to return</param>
		IQueryChain<T> Take(Expression<Func<int>> selector);
    }
}
