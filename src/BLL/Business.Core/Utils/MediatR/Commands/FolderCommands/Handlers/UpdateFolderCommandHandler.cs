namespace Business.Core.Utils.MediatR.Commands.FolderCommands.Handlers
{
    internal class UpdateFolderCommandHandler : BaseRequestHandler<UpdateFolderCommand, FolderDto>
    {
        #region Ctors

        public UpdateFolderCommandHandler(IDALQueryChain<MemeRepoClientContext> dal,
                                          IMapper mapper) : base(dal,
                                                                 mapper)
        {
        }

        #endregion

        public override async Task<FolderDto> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
        {
            var folder = _mapper.Map<Folder>(request);

            var createdFolder = await _dal.For<Folder>().Insert.InsertWithObjectAsync(folder);

            var result = _mapper.Map<FolderDto>(createdFolder);

            return result;
        }
    }
}
