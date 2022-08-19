using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions;

namespace Web.Core.Store.AppData.Actions.DataActionsEffects.AddFilesFromDiskActionsEffects
{
    internal class AddFilesFromDiskSuccessActionEffect : BaseSuccessActionEffect<AddFilesFromDiskSuccessAction>
    {
        public AddFilesFromDiskSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
