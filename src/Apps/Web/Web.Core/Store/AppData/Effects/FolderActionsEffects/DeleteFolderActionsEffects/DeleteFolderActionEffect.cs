using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Base.Store.Effects;
using Web.Core.Exceptions;
using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.DeleteFolderActionsEffects
{
    internal class DeleteFolderActionEffect : BaseDataEffect<DeleteFolderAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IState<AppDataState> _appDataState;

        #endregion

        #region Ctors

        public DeleteFolderActionEffect(IDALQueryChain<MemeRepoDbContext> dal, IMapper mapper, IFileStorageProvider fileStorageProvider, IState<AppDataState> appDataState) : base(dal, mapper)
        {
            _fileStorageProvider = fileStorageProvider;
            _appDataState = appDataState;
        }

        #endregion

        public override async Task HandleAsync(DeleteFolderAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetFolder = await _dal.For<Folder>().Get.FirstOrDefaultAsync(x => x.Id == action.Id);
                if (targetFolder == null)
                    throw new DeleteEntityException("Не найдена папка для удаления.");

                await _dal.For<Folder>().Delete.DeleteAsync(targetFolder);

                var path = _fileStorageProvider.GetAbsolutePath(_appDataState.Value.GetFolderRelativePath(action.Id));
                _fileStorageProvider.DeleteFolder(path);

                dispatcher.Dispatch(new DeleteFolderSuccessAction(targetFolder.Id));
            }
            catch (DeleteEntityException ex)
            {
                dispatcher.Dispatch(new DeleteFolderFailureAction
                {
                    FailureMessage = "Не удалось удалить папку.",
                    ErrorMessage = ex.Message,
                });
            }
            catch (Exception)
            {
                dispatcher.Dispatch(new DeleteFolderFailureAction
                {
                    FailureMessage = "Не удалось удалить папку.",
                    ErrorMessage = "",
                });
            }
        }
    }
}
