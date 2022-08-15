using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.UpdateFolderActionsEffects
{
    internal class UpdateFolderSuccessActionEffect : BaseSuccessActionEffect<CreateFolderSuccessAction>
    {
        public UpdateFolderSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
