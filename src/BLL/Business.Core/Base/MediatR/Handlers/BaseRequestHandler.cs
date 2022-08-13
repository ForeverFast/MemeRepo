namespace Business.Core.Base.MediatR.Handlers
{
    internal abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        #region Injects

        protected readonly IDALQueryChain<MemeRepoClientContext> _dal;
        protected readonly IMapper _mapper;

        #endregion

        #region Ctors

        protected BaseRequestHandler(IDALQueryChain<MemeRepoClientContext> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        #endregion

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
