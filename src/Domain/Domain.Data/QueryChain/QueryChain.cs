using Domain.Data.Interfaces.QueryChain;

namespace Domain.Data.QueryChain
{
    internal class QueryChain<T,F> : BaseQueryChain<T,F>, IQueryChain<T>
        where T : class
        where F : DataConnection
    {
        internal QueryChain(F context, IQueryable<T> query) : base(context, query)
        {
        }

        protected override IQueryChain<T> GetQueryChain() => this;
    }
}
