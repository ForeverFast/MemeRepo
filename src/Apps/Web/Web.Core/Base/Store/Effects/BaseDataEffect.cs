using DALQueryChain.Interfaces;
using Domain.Data.Context;

namespace Web.Core.Base.Store.Effects
{
    internal abstract class BaseDataEffect<T> : Effect<T> where T : class
    {
        #region Injects

        protected readonly IDALQueryChain<MemeRepoClientContext> _dal;
        protected readonly IMapper _mapper;

        #endregion

        #region Ctors

        protected BaseDataEffect(IDALQueryChain<MemeRepoClientContext> dal,
                                 IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        #endregion
    }
}
