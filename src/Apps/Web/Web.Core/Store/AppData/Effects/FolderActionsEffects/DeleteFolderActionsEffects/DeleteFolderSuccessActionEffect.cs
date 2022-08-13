using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.DeleteFolderActionsEffects
{
    internal class DeleteFolderSuccessActionEffect : BaseSuccessActionEffect<DeleteFolderSuccessAction>
    {
        public DeleteFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
