using Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.AppData.Effects.MemeActionsEffects.DeleteMemeActionsEffects
{
    internal class DeleteMemeSuccessActionEffect : BaseSuccessActionEffect<DeleteMemeSuccessAction>
    {
        public DeleteMemeSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
