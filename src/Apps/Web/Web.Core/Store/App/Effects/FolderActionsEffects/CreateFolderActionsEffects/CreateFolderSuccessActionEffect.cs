using Web.Core.Store.App.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.App.Effects.FolderActionsEffects.CreateFolderActionsEffects
{
    internal class CreateFolderSuccessActionEffect : BaseSuccessActionEffect<CreateFolderSuccessAction>
    {
        public CreateFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
