using DALQueryChain.Interfaces;

namespace Web.Core.Base.Store.Effects
{
    internal abstract class BaseDataEffect<T> : Effect<T> where T : class
    {
        #region Injects

        protected readonly IDALQueryChain<MemeRepoDbContext> _dal;
        protected readonly IMapper _mapper;

        #endregion

        #region Ctors

        protected BaseDataEffect(IDALQueryChain<MemeRepoDbContext> dal,
                                 IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        #endregion
    }
}
