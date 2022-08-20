using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.DeleteMemeActionsEffects
{
    internal class DeleteMemeFailureActionEffect : BaseFailureActionEffect<DeleteMemeFailureAction>
    {
        public DeleteMemeFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
