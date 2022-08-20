using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Exceptions;
using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.DeleteMemeActionsEffects
{
    internal class DeleteMemeActionEffect : BaseDataEffect<DeleteMemeAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IState<AppState> _appDataState;

        #endregion

        #region Ctors

        public DeleteMemeActionEffect(IDALQueryChain<MemeRepoDbContext> dal, IMapper mapper, IFileStorageProvider fileStorageProvider, IState<AppState> appDataState) : base(dal, mapper)
        {
            _fileStorageProvider = fileStorageProvider;
            _appDataState = appDataState;
        }

        #endregion

        public override async Task HandleAsync(DeleteMemeAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetMeme = await _dal.For<Meme>().Get.FirstOrDefaultAsync(x => x.Id == action.Id);
                if (targetMeme == null)
                    throw new DeleteEntityException("Не найден мем для удаления.");

                await _dal.For<Meme>().Delete.DeleteAsync(targetMeme);

                var relativeFilePath = _appDataState.Value.GetFileRelativePath(targetMeme.ParentFolderId!.Value, targetMeme.Path);
                var path = _fileStorageProvider.GetAbsolutePath(relativeFilePath);

                GC.Collect(1, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

                _fileStorageProvider.DeleteFile(path);

                dispatcher.Dispatch(new DeleteMemeSuccessAction(targetMeme.Id));
            }
            catch (DeleteEntityException ex)
            {
                dispatcher.Dispatch(new DeleteMemeFailureAction
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new DeleteMemeFailureAction
                {
                    ErrorMessage = "Не удалось удалить папку.",
                    Exception = ex,
                });
            }
        }
    }
}
