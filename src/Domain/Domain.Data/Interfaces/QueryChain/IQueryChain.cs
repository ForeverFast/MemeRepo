namespace Domain.Data.Interfaces.QueryChain
{
    public interface IQueryChain<T> 
        : IFilteredQueryChain<T>, IOrderedQueryChain<T>, ISelectableQueryChain<T>, IExecutableQueryChain<T>
    {
		
    }
}
