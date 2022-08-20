using Web.Core.Store.App.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.UpdateTagActionsEffects
{
    internal class UpdateTagSuccessActionEffect : BaseSuccessActionEffect<UpdateTagSuccessAction>
    {
        public UpdateTagSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
