using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.DeleteMemeActionsEffects
{
    internal class DeleteMemeSuccessActionEffect : BaseSuccessActionEffect<DeleteMemeSuccessAction>
    {
        public DeleteMemeSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
