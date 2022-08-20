using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;

namespace Web.Core.Store.App.Actions.DataActionsEffects.AddFilesFromDiskActionsEffects
{
    internal class AddFilesFromDiskFailureActionEffect : BaseFailureActionEffect<AddFilesFromDiskFailureAction>
    {
        public AddFilesFromDiskFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
