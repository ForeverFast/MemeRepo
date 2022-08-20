using Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.App.Effects.FolderActionsEffects.DeleteFolderActionsEffects
{
    internal class DeleteFolderFailureActionEffect : BaseFailureActionEffect<DeleteFolderFailureAction>
    {
        public DeleteFolderFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
