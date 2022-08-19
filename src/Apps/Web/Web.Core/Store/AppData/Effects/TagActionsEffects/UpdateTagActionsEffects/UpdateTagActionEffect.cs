using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Components.DialogComponents;
using Web.Core.Exceptions;
using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.UpdateTagActionsEffects
{
    internal class UpdateTagActionEffect : BaseDataEffect<UpdateTagAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IDialogService _dialogService;
        protected readonly IState<AppDataState> _appDataState;

        #endregion

        #region Ctors

        public UpdateTagActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
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

        public override async Task HandleAsync(UpdateTagAction action, IDispatcher dispatcher)
        {
            try
            {
                var tag = await _dal.For<Tag>().Get.FirstOrDefaultAsync(x => x.Id == action.Id);
                if (tag == null)
                    throw new UpdateEntityException("Тег не найден в локальном хранилище.");

                var dialog = _dialogService!.Show<TagDialog>("Обновить тег", TagDialog.DialogOptions);
                var dialogResult = await dialog.Result;

                if (dialogResult.Cancelled)
                    return;

                var tagDialogResult = (TagDialogViewModel)dialogResult.Data;
                var updatedFromDialogTag = _mapper.Map<Tag>(tagDialogResult);

                await _dal.For<Tag>().Update.UpdateAsync(updatedFromDialogTag);

                var result = _mapper.Map<TagViewModel>(updatedFromDialogTag);

                dispatcher.Dispatch(new UpdateTagSuccessAction(result)
                {
                    SuccessMessage = $"Тег успешно обновлён",
                });
            }
            catch (UpdateEntityException ex)
            {
                dispatcher.Dispatch(new UpdateTagFailureAction
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new UpdateTagFailureAction
                {
                    ErrorMessage = "При обновление мема возникла ошибка",
                    Exception = ex,
                });
            }
        }
    }
}
