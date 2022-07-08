using Clients.Data.Context;
using Clients.Data.Interfaces;
using Domain.Data.QueryChain;

namespace Clients.Data
{
    internal class DataAccessManager : IDataAccessManager<MemeRepoClientContext>
    {
        #region Injects

        private readonly MemeRepoClientContext _context;

        #endregion

        #region Ctors

        public DataAccessManager(MemeRepoClientContext context)
        {
            _context = context;
            _context.CommandTimeout = 120;
        }

        #endregion

        private BuildQuery<MemeRepoClientContext> _query;
        public BuildQuery<MemeRepoClientContext> Query => _query ??= new BuildQuery<MemeRepoClientContext>(_context);
    }
}
