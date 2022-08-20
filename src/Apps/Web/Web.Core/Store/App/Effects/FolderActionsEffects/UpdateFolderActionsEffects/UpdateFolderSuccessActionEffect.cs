using Web.Core.Store.App.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.App.Effects.FolderActionsEffects.UpdateFolderActionsEffects
{
    internal class UpdateFolderSuccessActionEffect : BaseSuccessActionEffect<UpdateFolderSuccessAction>
    {
        public UpdateFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
