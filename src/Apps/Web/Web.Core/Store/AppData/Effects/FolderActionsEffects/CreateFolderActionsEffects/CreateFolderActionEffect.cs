using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Components.DialogComponents;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.CreateFolderActionsEffects
{
    internal class CreateFolderActionEffect : BaseDataEffect<CreateFolderAction>
    {
        #region Injects

        protected readonly IState<AppDataState> _appDataState;
        protected readonly IDialogService _dialogService;
        protected readonly IFileStorageProvider _fileStorageProvider;

        #endregion

        #region Ctors

        public CreateFolderActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
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

        public override async Task HandleAsync(CreateFolderAction action, IDispatcher dispatcher)
        {
            try
            {
                var dialog = _dialogService!.Show<FolderDialog>("Создать папку", FolderDialog.DialogOptions);
                var dialogResult = await dialog.Result;

                if (dialogResult.Cancelled)
                    return;

                var folderDialogResult = (FolderDialogViewModel)dialogResult.Data;
                var folder = _mapper.Map<Folder>(folderDialogResult);
                folder.ParentFolderId = action.ParentFolderId;

                var absoluteParentFolderPath = _fileStorageProvider.GetAbsolutePath(_appDataState.Value.GetFolderRelativePath(action.ParentFolderId));
                var absoluteNewFolderPath = _fileStorageProvider.CreateFolderPath(absoluteParentFolderPath, folder.Title);
                folder.Path = new DirectoryInfo(absoluteNewFolderPath).Name;

                var createdFolder = await _dal.For<Folder>().Insert.InsertWithObjectAsync(folder);
                var newTagCollection = folderDialogResult.Tags.Select(x => new FolderTag
                {
                    FolderId = createdFolder.Id,
                    TagId = x,
                });
                await _dal.For<FolderTag>().Insert.BulkInsertAsync(newTagCollection);

                _fileStorageProvider.CreateFolder(absoluteNewFolderPath);

                var result = _mapper.Map<FolderTreeViewModel>(createdFolder);

                dispatcher.Dispatch(new CreateFolderSuccessAction(result));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new CreateFolderFailureAction
                {
                    ErrorMessage = "Ошибка при создании папки",
                    Exception = ex,
                });
            }
        }
    }
}
