using System.Linq.Expressions;

namespace Domain.Data.Interfaces.QueryChain
{
    public interface IExecutableQueryChain<T>
    {
		#region Sync 

		/// <summary>
		/// Returns the first element of a sequence, or a default value if the sequence contains no elements
		/// </summary>
		T? FirstOrDefault();

		/// <summary>
		/// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		T? FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
		/// Returns the first element of a sequence
		/// </summary>
		T First();

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        T First(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        T? SingleOrDefault();

		/// <summary>
		/// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists;
		/// this method throws an exception if more than one element satisfies the condition.
		/// </summary>
		/// <param name="predicate">A function to test an element for a condition</param>
		T? SingleOrDefault(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Returns the last element of a sequence, or a default value if the sequence contains no elements
		/// </summary>
		T? LastOrDefault();

		/// <summary>
		/// Returns the last element of a sequence that satisfies a condition or a default value if no such element is found
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		T? LastOrDefault(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Creates a System.Collections.Generic.List`1 from an query
		/// </summary>
		List<T> ToList();

		/// <summary>
		/// Returns the number of elements in a sequence
		/// </summary>
		int Count();

		/// <summary>
		/// Returns a number that represents how many elements in the specified sequence satisfy a condition
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		int Count(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Determines whether a sequence contains any elements
		/// </summary>
		bool Any();

		/// <summary>
		/// Determines whether any element of a sequence satisfies a condition
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		bool Any(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Determines whether all elements of a sequence satisfy a condition
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		bool All(Expression<Func<T, bool>> predicate);

        #endregion

        #region Async

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements
        /// </summary>
        Task<T?> FirstOrDefaultAsync();

		/// <summary>
		/// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the first element of a sequence
        /// </summary>
        Task<T> FirstAsync();

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        Task<T?> SingleOrDefaultAsync();

		/// <summary>
		/// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists;
		/// this method throws an exception if more than one element satisfies the condition.
		/// </summary>
		/// <param name="predicate">A function to test an element for a condition</param>
		Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Creates a System.Collections.Generic.List`1 from an query
		/// </summary>
		Task<List<T>> ToListAsync();

		/// <summary>
		/// Returns the number of elements in a sequence
		/// </summary>
		Task<int> CountAsync();

		/// <summary>
		/// Returns a number that represents how many elements in the specified sequence satisfy a condition
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		Task<int> CountAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Determines whether a sequence contains any elements
		/// </summary>
		Task<bool> AnyAsync();

		/// <summary>
		/// Determines whether any element of a sequence satisfies a condition
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Determines whether all elements of a sequence satisfy a condition
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition</param>
		Task<bool> AllAsync(Expression<Func<T, bool>> predicate);

        #endregion
    }
}
