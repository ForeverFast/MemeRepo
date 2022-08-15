using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Base.Store.Effects;
using Web.Core.Components.DialogComponents;
using Web.Core.Exceptions;
using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Store.AppData.Effects.MemeActionsEffects.UpdateMemeActionsEffects
{
    internal class UpdateMemeActionEffect : BaseDataEffect<UpdateMemeAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IDialogService _dialogService;
        protected readonly IState<AppDataState> _appDataState;

        #endregion

        #region Ctors

        public UpdateMemeActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
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

        public override async Task HandleAsync(UpdateMemeAction action, IDispatcher dispatcher)
        {
            try
            {
                var newPathFlag = false;
                var absoluteOldFilePath = string.Empty;
                var absoluteTmpFilePath = string.Empty;
                var absoluteNewFilePath = string.Empty;

                var meme = await _dal.For<Meme>().Get
                    .LoadWith(x => x.MemeTags)
                    .FirstOrDefaultAsync(x => x.Id == action.Id);
                if (meme == null)
                    throw new UpdateEntityException("Мем не найденеа в локальном хранилище.");

                var memeDialogVM = _mapper!.Map<MemeDialogViewModel>(meme);
                var dialopParams = new DialogParameters
                {
                    { nameof(MemeDialog.Meme), memeDialogVM }
                };

                var dialog = _dialogService!.Show<MemeDialog>("Обновить мем", dialopParams, MemeDialog.DialogOptions);
                var dialogResult = await dialog.Result;

                if (dialogResult.Cancelled)
                    return;

                var memeDialogResult = (MemeDialogViewModel)dialogResult.Data;
                var updatedFromDialogMeme = _mapper.Map<Meme>(memeDialogResult);

                if (!string.IsNullOrEmpty(memeDialogResult.Path))
                {
                    absoluteTmpFilePath = memeDialogResult.Path;

                    var folderRelativePath = _appDataState.Value.GetFolderRelativePath(meme.ParentFolderId);
                    var absoluteParentFolderPath = _fileStorageProvider.GetAbsolutePath(folderRelativePath);
                    absoluteOldFilePath = Path.Combine(absoluteParentFolderPath, meme.Path);

                    absoluteNewFilePath = _fileStorageProvider.CreateFilePath(absoluteParentFolderPath,
                        new FileInfo(absoluteTmpFilePath).Extension,
                        updatedFromDialogMeme.Title);

                    meme.Path = new FileInfo(absoluteNewFilePath).Name;

                    newPathFlag = true;
                }

                await _dal.For<Meme>().Update.UpdateAsync(updatedFromDialogMeme);
                await _dal.For<MemeTag>().Delete.BulkDeleteAsync(meme.MemeTags);
                var newTagCollection = memeDialogResult.Tags.Select(x => new MemeTag
                {
                    MemeId = meme.Id,
                    TagId = x,
                });
                await _dal.For<MemeTag>().Insert.BulkInsertAsync(newTagCollection);

                if (newPathFlag)
                {
                    _fileStorageProvider.DeleteFile(absoluteOldFilePath);
                    _fileStorageProvider.CopyFileToNewFile(absoluteTmpFilePath, absoluteNewFilePath);
                }

                var result = _mapper.Map<MemeViewModel>(updatedFromDialogMeme);

                dispatcher.Dispatch(new UpdateMemeSuccessAction(result)
                {
                    SuccessMessage = $"Мем \"{result.Title}\" успешно обновлён",
                });
            }
            catch (UpdateEntityException ex)
            {
                dispatcher.Dispatch(new UpdateMemeFailureAction
                {
                    FailureMessage = "При обновление мема возникла ошибка",
                    ErrorMessage = ex.Message,
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new UpdateMemeFailureAction
                {
                    FailureMessage = "При обновление мема возникла ошибка",
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
