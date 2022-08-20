using Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.App.Effects.FolderActionsEffects.DeleteFolderActionsEffects
{
    internal class DeleteFolderSuccessActionEffect : BaseSuccessActionEffect<DeleteFolderSuccessAction>
    {
        public DeleteFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
