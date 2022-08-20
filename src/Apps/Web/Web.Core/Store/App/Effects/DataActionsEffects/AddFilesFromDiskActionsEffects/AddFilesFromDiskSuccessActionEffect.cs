using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;

namespace Web.Core.Store.App.Actions.DataActionsEffects.AddFilesFromDiskActionsEffects
{
    internal class AddFilesFromDiskSuccessActionEffect : BaseSuccessActionEffect<AddFilesFromDiskSuccessAction>
    {
        public AddFilesFromDiskSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
