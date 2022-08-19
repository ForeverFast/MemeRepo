using Web.Core.Store.AppData.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.UpdateTagActionsEffects
{
    internal class UpdateTagSuccessActionEffect : BaseSuccessActionEffect<UpdateTagSuccessAction>
    {
        public UpdateTagSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
