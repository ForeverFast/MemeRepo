using DALQueryChain.Interfaces;
using Domain.Core.Interfaces;
using Web.Core.Exceptions;
using Web.Core.Store.App.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.DeleteTagActionsEffects
{
    internal class DeleteTagActionEffect : BaseDataEffect<DeleteTagAction>
    {
        #region Injects

        protected readonly IFileStorageProvider _fileStorageProvider;
        protected readonly IState<AppState> _appDataState;

        #endregion

        #region Ctors

        public DeleteTagActionEffect(IDALQueryChain<MemeRepoDbContext> dal, IMapper mapper, IFileStorageProvider fileStorageProvider, IState<AppState> appDataState) : base(dal, mapper)
        {
            _fileStorageProvider = fileStorageProvider;
            _appDataState = appDataState;
        }

        #endregion

        public override async Task HandleAsync(DeleteTagAction action, IDispatcher dispatcher)
        {
            try
            {
                var tag = await _dal.For<Tag>().Get.FirstOrDefaultAsync(x => x.Id == action.Id);
                if (tag == null)
                    throw new DeleteEntityException("Тег не найден в локальном хранилище.");

                await _dal.For<Tag>().Delete.DeleteAsync(tag);

                dispatcher.Dispatch(new DeleteTagSuccessAction(tag.Id));
            }
            catch (DeleteEntityException ex)
            {
                dispatcher.Dispatch(new DeleteTagFailureAction
                {
                    ErrorMessage = ex.Message,
                    Exception = ex,
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new DeleteTagFailureAction
                {
                    ErrorMessage = "Не удалось удалить папку.",
                    Exception = ex,
                });
            }
        }
    }
}
