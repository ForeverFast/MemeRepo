using Web.Core.Store.App.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.DeleteTagActionsEffects
{
    internal class DeleteTagFailureActionEffect : BaseFailureActionEffect<DeleteTagFailureAction>
    {
        public DeleteTagFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
