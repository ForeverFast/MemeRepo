using Web.Core.Store.App.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.UpdateTagActionsEffects
{
    internal class UpdateTagFailureActionEffect : BaseFailureActionEffect<UpdateTagFailureAction>
    {
        public UpdateTagFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
