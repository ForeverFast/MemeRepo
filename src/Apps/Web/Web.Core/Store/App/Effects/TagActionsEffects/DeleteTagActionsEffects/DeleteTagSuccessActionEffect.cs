using Web.Core.Store.App.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.DeleteTagActionsEffects
{
    internal class DeleteTagSuccessActionEffect : BaseSuccessActionEffect<DeleteTagSuccessAction>
    {
        public DeleteTagSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
