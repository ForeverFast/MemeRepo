using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.UpdateFolderActionsEffects
{
    internal class UpdateFolderFailureActionEffect : BaseFailureActionEffect<UpdateFolderFailureAction>
    {
        public UpdateFolderFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
