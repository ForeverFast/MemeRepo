using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Components.DialogComponents;
using Web.Core.Exceptions;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.UpdateFolderActionsEffects
{
    internal class UpdateFolderActionEffect : BaseDataEffect<UpdateFolderAction>
    {
        #region Injects

        protected readonly IState<AppDataState> _appDataState;
        protected readonly IDialogService _dialogService;
        protected readonly IFileStorageProvider _fileStorageProvider;

        #endregion

        #region Ctors

        public UpdateFolderActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
                                        IMapper mapper,
                                        IFileStorageProvider fileStorageProvider,
                                        IState<AppDataState> appDataState,
                                        IDialogService dialogService) : base(dal, mapper)
        {
            _fileStorageProvider = fileStorageProvider;
            _appDataState = appDataState;
            _dialogService = dialogService;
        }

        #endregion

        public override async Task HandleAsync(UpdateFolderAction action, IDispatcher dispatcher)
        {
            try
            {
                var folder = await _dal.For<Folder>().Get
                    .LoadWith(x => x.FolderTags)
                    .FirstOrDefaultAsync(x => x.Id == action.Id);
                if (folder == null)
                    throw new UpdateEntityException("Папка не найденеа в локальном хранилище.");

                var targetFolder = _mapper!.Map<FolderDialogViewModel>(folder);
                var dialopParams = new DialogParameters
                {
                    { nameof(FolderDialog.Folder), targetFolder }
                };

                var dialog = _dialogService!.Show<FolderDialog>("Обновить папку", dialopParams, FolderDialog.DialogOptions);
                var dialogResult = await dialog.Result;

                if (dialogResult.Cancelled)
                    return;

                var folderDialogResult = (FolderDialogViewModel)dialogResult.Data;
                var updatedFromDialogFolder = _mapper.Map<Folder>(folderDialogResult);

                await _dal.For<Folder>().Update.UpdateAsync(updatedFromDialogFolder);
                await _dal.For<FolderTag>().Delete.BulkDeleteAsync(folder.FolderTags);
                var newTagCollection = folderDialogResult.Tags.Select(x => new FolderTag
                {
                    FolderId = folder.Id,
                    TagId = x,
                });
                await _dal.For<FolderTag>().Insert.BulkInsertAsync(newTagCollection);

                var result = _mapper.Map<FolderTreeViewModel>(updatedFromDialogFolder);

                dispatcher.Dispatch(new UpdateFolderSuccessAction(result)
                {
                    SuccessMessage = $"Папка \"{result.Title}\" успешно обновлена",
                });
            }
            catch (UpdateEntityException ex)
            {
                dispatcher.Dispatch(new UpdateFolderFailureAction
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new UpdateFolderFailureAction
                {
                    ErrorMessage = "При обновление папки возникла ошибка",
                    Exception = ex,
                });
            }
        }
    }
}
