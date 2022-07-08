using Domain.Data.QueryChain;
using LinqToDB.Data;

namespace Clients.Data.Interfaces
{
    internal interface IDataAccessManager<F> where F : DataConnection
    {
        BuildQuery<F> Query { get; }
    }
}
