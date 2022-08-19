using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Components.DialogComponents;
using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.AppData.Effects.MemeActionsEffects.CreateMemeActionsEffects
{
    internal class CreateMemeActionEffect : BaseDataEffect<CreateMemeAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IDialogService _dialogService;
        protected readonly IState<AppDataState> _appDataState;

        #endregion

        #region Ctors

        public CreateMemeActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
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

        public override async Task HandleAsync(CreateMemeAction action, IDispatcher dispatcher)
        {
            try
            {
                var dialog = _dialogService!.Show<MemeDialog>("Создать мем", MemeDialog.DialogOptions);
                var dialogResult = await dialog.Result;

                if (dialogResult.Cancelled)
                    return;

                var memeDialogResult = (MemeDialogViewModel)dialogResult.Data;
                var meme = _mapper.Map<Meme>(memeDialogResult);
                meme.ParentFolderId = action.ParentFolderId;

                var absoluteTmpFilePath = meme.Path;
                var folderRelativePath = _appDataState.Value.GetFolderRelativePath(action.ParentFolderId);
                var absoluteParentFolderPath = _fileStorageProvider.GetAbsolutePath(folderRelativePath);
                var absoluteFilePath = _fileStorageProvider.CreateFilePath(
                    absoluteParentFolderPath, new FileInfo(absoluteTmpFilePath).Extension, meme.Title);
                meme.Path = new FileInfo(absoluteFilePath).Name;

                var createdMeme = await _dal.For<Meme>().Insert.InsertWithObjectAsync(meme);
                var newTagCollection = memeDialogResult.Tags.Select(x => new MemeTag
                {
                    MemeId = createdMeme.Id,
                    TagId = x,
                });
                await _dal.For<MemeTag>().Insert.BulkInsertAsync(newTagCollection);

                _fileStorageProvider.CopyFileToNewFile(absoluteTmpFilePath, absoluteFilePath);

                var result = _mapper.Map<MemeViewModel>(createdMeme);

                dispatcher.Dispatch(new CreateMemeSuccessAction(result)
                {
                    SuccessMessage = "Мем успешно создан",
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new CreateMemeFailureAction
                {
                    ErrorMessage = "Ошибка при создании мема",
                    Exception = ex,
                });
            }
        }
    }
}
