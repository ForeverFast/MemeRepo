using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.CreateFolderActionsEffects
{
    internal class CreateFolderFailureActionEffect : BaseFailureActionEffect<CreateFolderFailureAction>
    {
        public CreateFolderFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
