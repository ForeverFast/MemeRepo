namespace Business.Core.Utils.MediatR.Queries.GlobalQueries.Handlers
{
    internal class GetAppDataHandler : BaseRequestHandler<GetAppDataQuery, AppDataDto>
    {
        #region Ctors

        public GetAppDataHandler(IDALQueryChain<MemeRepoClientContext> dal,
                                 IMapper mapper) : base(dal,
                                                        mapper)
        {
        }

        #endregion

        public override async Task<AppDataDto> Handle(GetAppDataQuery request, CancellationToken cancellationToken)
        {
            var folders = await _dal.For<Folder>()
                .Get
                .LoadWith(x => x.Tags)
                .ToListAsync();

            var tags = await _dal.For<Tag>()
                .Get
                .ToListAsync();

            var result = new AppDataDto
            {
                Folders = _mapper.Map<HashSet<FolderDto>>(folders),
                Tags = _mapper.Map<HashSet<TagDto>>(tags)
            };

            return result;
        }
    }
}
