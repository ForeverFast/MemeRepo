using System.Linq.Expressions;

namespace Domain.Data.Interfaces.QueryChain
{
    public interface IOrderedQueryChain<T>
    {
        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key
        /// </summary>
        /// <param name="selector">A function to extract a key from each element</param>
        IQueryChain<T> ThenBy(Expression<Func<T, object>> selector);

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key
        /// </summary>
        /// <param name="selector">A function to extract a key from each element</param>
        IQueryChain<T> ThenByDescending(Expression<Func<T, object>> selector);

	}
}
