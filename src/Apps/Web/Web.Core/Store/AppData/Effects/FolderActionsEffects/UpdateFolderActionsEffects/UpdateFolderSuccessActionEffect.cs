using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.UpdateFolderActionsEffects
{
    internal class UpdateFolderSuccessActionEffect : BaseSuccessActionEffect<UpdateFolderSuccessAction>
    {
        public UpdateFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
