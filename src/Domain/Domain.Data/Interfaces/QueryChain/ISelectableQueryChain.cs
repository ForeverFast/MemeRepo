using System.Linq.Expressions;

namespace Domain.Data.Interfaces.QueryChain
{
    public interface ISelectableQueryChain<T>
    {
        IQueryChain<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class;
        IQueryChain<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector) where TResult : class;
    }
}
