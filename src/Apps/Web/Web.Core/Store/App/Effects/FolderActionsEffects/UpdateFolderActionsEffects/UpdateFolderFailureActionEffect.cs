using Web.Core.Store.App.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.App.Effects.FolderActionsEffects.UpdateFolderActionsEffects
{
    internal class UpdateFolderFailureActionEffect : BaseFailureActionEffect<UpdateFolderFailureAction>
    {
        public UpdateFolderFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
