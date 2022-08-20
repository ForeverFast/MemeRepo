using Web.Core.Store.App.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.App.Effects.FolderActionsEffects.CreateFolderActionsEffects
{
    internal class CreateFolderFailureActionEffect : BaseFailureActionEffect<CreateFolderFailureAction>
    {
        public CreateFolderFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
