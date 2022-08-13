using DALQueryChain.Interfaces;
using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.DeleteFolderActionsEffects
{
    internal class DeleteFolderActionEffect : BaseDataEffect<DeleteFolderAction>
    {
        #region Ctors

        public DeleteFolderActionEffect(IDALQueryChain<MemeRepoClientContext> dal, IMapper mapper) : base(dal, mapper)
        {
        }

        #endregion

        public override async Task HandleAsync(DeleteFolderAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetFolder = await _dal.For<Folder>().Get.FirstOrDefaultAsync(x => x.Id == action.FolderId);
                if (targetFolder == null)
                {
                    return;
                }

                await _dal.For<Folder>().Delete.DeleteAsync(targetFolder);
   
                dispatcher.Dispatch(new DeleteFolderSuccessAction(targetFolder.Id));
            }
            catch (Exception)
            {
                dispatcher.Dispatch(new DeleteFolderFailureAction
                {
                    FailureMessage = "",
                    ErrorMessage = "Failed to create Folder"
                });
            }
        }
    }
}
