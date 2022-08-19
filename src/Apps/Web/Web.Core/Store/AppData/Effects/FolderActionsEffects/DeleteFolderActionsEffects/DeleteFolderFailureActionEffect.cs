using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.DeleteFolderActionsEffects
{
    internal class DeleteFolderFailureActionEffect : BaseFailureActionEffect<DeleteFolderFailureAction>
    {
        public DeleteFolderFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
