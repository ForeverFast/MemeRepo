using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.UpdateMemeActionsEffects
{
    internal class UpdateMemeSuccessActionEffect : BaseSuccessActionEffect<UpdateMemeSuccessAction>
    {
        public UpdateMemeSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
