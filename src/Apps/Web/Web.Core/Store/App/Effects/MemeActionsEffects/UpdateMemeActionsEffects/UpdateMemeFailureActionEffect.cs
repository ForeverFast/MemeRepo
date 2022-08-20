using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.UpdateMemeActionsEffects
{
    internal class UpdateMemeFailureActionEffect : BaseFailureActionEffect<UpdateMemeFailureAction>
    {
        public UpdateMemeFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
