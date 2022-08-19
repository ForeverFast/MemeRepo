using Web.Core.Store.AppData.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.UpdateTagActionsEffects
{
    internal class UpdateTagFailureActionEffect : BaseFailureActionEffect<UpdateTagFailureAction>
    {
        public UpdateTagFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
