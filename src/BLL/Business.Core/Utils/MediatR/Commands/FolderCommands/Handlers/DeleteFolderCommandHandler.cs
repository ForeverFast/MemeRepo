namespace Business.Core.Utils.MediatR.Commands.FolderCommands.Handlers
{
    internal class DeleteFolderCommandHandler : BaseRequestHandler<DeleteFolderCommand, bool>
    {
        #region Ctors

        public DeleteFolderCommandHandler(IDALQueryChain<MemeRepoClientContext> dal,
                                          IMapper mapper) : base(dal,
                                                                 mapper)
        {
        }

        #endregion

        public override async Task<bool> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var target = await _dal.For<Folder>().Get.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (target == null)
                return false;

            await _dal.For<Folder>().Delete.DeleteAsync(target);

            return true;
        }
    }
}
