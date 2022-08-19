using Web.Core.Store.AppData.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.DeleteTagActionsEffects
{
    internal class DeleteTagFailureActionEffect : BaseFailureActionEffect<DeleteTagFailureAction>
    {
        public DeleteTagFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
