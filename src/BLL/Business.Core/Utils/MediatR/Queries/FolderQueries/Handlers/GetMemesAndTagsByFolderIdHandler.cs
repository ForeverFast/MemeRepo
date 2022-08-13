namespace Business.Core.Utils.MediatR.Queries.FolderQueries.Handlers
{
    internal class GetMemesAndTagsByFolderIdHandler : BaseRequestHandler<GetMemesAndTagsByFolderIdQuery, (List<MemeDto> Memes, List<TagDto> Tags)>
    {
        #region Ctors

        public GetMemesAndTagsByFolderIdHandler(IDALQueryChain<MemeRepoClientContext> dal,
                                                IMapper mapper) : base(dal,
                                                                       mapper)
        {
        }

        #endregion

        public override async Task<(List<MemeDto> Memes, List<TagDto> Tags)> Handle(GetMemesAndTagsByFolderIdQuery request,
            CancellationToken cancellationToken)
        {
            var folder = await _dal.For<Folder>()
                .Get
                .LoadWith(x => x.Tags)
                .LoadWith(x => x.Memes)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            (List<MemeDto> Memes, List<TagDto> Tags) result = new(new List<MemeDto>(), new List<TagDto>());

            if (folder == null)
                return result;

            result.Memes = _mapper.Map<List<MemeDto>>(folder.Memes);
            result.Tags = _mapper.Map<List<TagDto>>(folder.Tags);

            return result;

        }
    }
}
