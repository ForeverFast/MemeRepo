using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Components.DialogComponents;
using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.App.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.CreateTagActionsEffects
{
    internal class CreateTagActionEffect : BaseDataEffect<CreateTagAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IDialogService _dialogService;
        protected readonly IState<AppState> _appDataState;

        #endregion

        #region Ctors

        public CreateTagActionEffect(IDALQueryChain<MemeRepoDbContext> dal,
                                        IMapper mapper,
                                        IFileStorageProvider fileStorageProvider,
                                        IState<AppState> appDataState,
                                        IDialogService dialogService) : base(dal, mapper)
        {
            _fileStorageProvider = fileStorageProvider;
            _appDataState = appDataState;
            _dialogService = dialogService;
        }

        #endregion

        public override async Task HandleAsync(CreateTagAction action, IDispatcher dispatcher)
        {
            try
            {
                var dialog = _dialogService!.Show<TagDialog>("Создать тег", TagDialog.DialogOptions);
                var dialogResult = await dialog.Result;

                if (dialogResult.Cancelled)
                    return;

                var tagDialogResult = (TagDialogViewModel)dialogResult.Data;
                var tag = _mapper.Map<Tag>(tagDialogResult);
              
                var createdTag = await _dal.For<Tag>().Insert.InsertWithObjectAsync(tag);
               
                var result = _mapper.Map<TagViewModel>(createdTag);

                dispatcher.Dispatch(new CreateTagSuccessAction(result)
                {
                    SuccessMessage = "Тег успешно создан",
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new CreateTagFailureAction
                {
                    ErrorMessage = "Ошибка при создании тега",
                    Exception = ex,
                });
            }
        }
    }
}
