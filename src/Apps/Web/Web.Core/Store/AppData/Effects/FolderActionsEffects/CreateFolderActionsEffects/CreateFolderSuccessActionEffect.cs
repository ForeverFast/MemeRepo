using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.CreateFolderActionsEffects
{
    internal class CreateFolderSuccessActionEffect : BaseSuccessActionEffect<CreateFolderSuccessAction>
    {
        public CreateFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
