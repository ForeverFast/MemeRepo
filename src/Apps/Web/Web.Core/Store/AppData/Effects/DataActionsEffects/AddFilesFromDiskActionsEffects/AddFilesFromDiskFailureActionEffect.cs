using Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions;

namespace Web.Core.Store.AppData.Actions.DataActionsEffects.AddFilesFromDiskActionsEffects
{
    internal class AddFilesFromDiskFailureActionEffect : BaseFailureActionEffect<AddFilesFromDiskFailureAction>
    {
        public AddFilesFromDiskFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
